using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;

namespace CinemaManagement.AdminWpf.ViewModels.Pages
{
    public partial class ReportViewModel : ObservableValidator
    {
        private readonly MovieService _movieService;
        private readonly ScreeningService _screeningService;
        private readonly TicketService _ticketService;


        public ReportViewModel(
            MovieService movieService,
            ScreeningService screeningService,
            TicketService ticketService
        )
        {
            _movieService = movieService;
            _screeningService = screeningService;
            _ticketService = ticketService;
        }


        [ObservableProperty]
        private string _movieSearch = string.Empty;
        async partial void OnMovieSearchChanged(string value)
        {
            await FindMovie();
        }

        public ObservableCollection<Screening> Screenings { get; } = [];

        public int _ticketsSold => DataPoints.Sum(t => t.Item3);

        public int TotalRevenue => DataPoints.Sum(t => t.Item2);

        public List<Tuple<DateTime, int, int>> DataPoints { get; private set; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MovieFound))]
        private Movie? _selectedMovie;
        public bool MovieFound => SelectedMovie is not null;

        [ObservableProperty]
        private int? _selectedScreeningId;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _isGenerated = false;


        [RelayCommand]
        private async Task GenerateReport()
        {
            IsLoading = true;

            await LoadTickets();
            OnPropertyChanged(nameof(DataPoints));

            IsLoading = false;
        }


        private async Task FindMovie()
        {
            if (string.IsNullOrWhiteSpace(MovieSearch))
            {
                SelectedMovie = null;
                return;
            }
            SelectedMovie = await Task.Run(() => _movieService.FindMovie(MovieSearch));

            await LoadScreenings();
        }

        private async Task LoadScreenings()
        {
            if (SelectedMovie is null) { return; }

            var res = await Task.Run(() => _screeningService.GetAllScreeningsOfMovie(SelectedMovie.Id));
            Screenings.Clear();
            Screenings.Add(new Screening());
            foreach (var screening in res)
            {
                Screenings.Add(screening);
            }
        }

        private async Task LoadTickets()
        {
            if (SelectedMovie is null)
            {
                DataPoints = await Task.Run(() => _ticketService.GetRevenueByDate());
            }
            else if (SelectedScreeningId is null)
            {
                DataPoints = await Task.Run(() => _ticketService.GetRevenueByDateForMovie(SelectedMovie.Id));
            }
            else
            {
                DataPoints = await Task.Run(() => _ticketService.GetRevenueByDateForScreening(SelectedScreeningId.Value));
            }

            Series = new ISeries[]
            {
                new LineSeries<int>
                {
                    Values = DataPoints.Select(t => t.Item2).ToArray(),
                    Name = "Revenue",
                    Fill = null
                },
            };
            OnPropertyChanged(nameof(Series));

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = DataPoints.Select(t => t.Item1.ToShortDateString()).ToArray()
                }
            };
            OnPropertyChanged(nameof(XAxes));

            OnPropertyChanged(nameof(TotalRevenue));
            IsGenerated = true;
        }



        #region Chart

        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }


        #endregion Chart
    }
}
