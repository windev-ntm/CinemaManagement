using CinemaManagement.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Input;
using CinemaManagement.View;
using System;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace CinemaManagement.ViewModel
{

    public class ScreeningInfo : INotifyPropertyChanged
    {
        public TimeSpan Duration { get; set; }
        public Screening screening { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ScreenName { 
            get => this.screening.Screen.Name; 
        }

        private int _totalSeat;
        public int TotalSeat
        {
            get => _totalSeat;
            set
            {
                _totalSeat = value;
                OnPropertyChanged(nameof(TotalSeat));
            }
        }
        private int _remainingSeat;
        public int RemainingSeat
        {
            get => _remainingSeat;
            set
            {
                _remainingSeat = value;
                OnPropertyChanged(nameof(RemainingSeat));
            }
        }


        public ScreeningInfo(Screening screening, TimeSpan duration)
        {
            this.TotalSeat = (int)screening.Screen.Seats;
            this._remainingSeat = TotalSeat - screening.Tickets.Count;
            this.Duration = duration;
            this.screening = screening;
            if (screening.StartTime.HasValue)
            {
                this.Date = screening.StartTime.Value.ToString("dd/MM/yy");
                this.StartTime = screening.StartTime.Value.ToString("HH:mm"); // Lấy phần thời gian

                TimeSpan Tong = screening.StartTime.Value.TimeOfDay + duration;
                this.EndTime = new DateTime(Tong.Ticks).ToString("HH:mm");



            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    class MovieDetailViewModel : INotifyPropertyChanged
    {
        private User user=null;
        private Movie movie;
        private MovieService movieService;

        public MovieDetailViewModel(Movie movie, User user)
        {
            this.user = user;
            this.movie = movie;
            this.movieService = new MovieService();
            this.movie= movie;
            InitComponent(movie);
        }
        private void InitComponent(Movie movie)
        {
            this._actorList = movieService.GetActors(movie);
            this._directorList = movieService.GetDirectors(movie);


            // Init Screening
            this._screeningList.Clear();
            List<Screening> screeningList = movieService.GetScreenings(movie);
            for (int i = 0; i < screeningList.Count; i++)
            {
                ScreeningInfo screeningInfo = new ScreeningInfo(screeningList[i], (TimeSpan)movie.Duration);
                this._screeningList.Add(screeningInfo);
            }

        }

        private ICommand _playButton;
        private ICommand _buyButton;
        private ICommand _screeningSelected;
        private ICommand _screeningSelectedCommand;

        private List<string> _actorList;
        private List<string> _directorList;
        private ObservableCollection<ScreeningInfo> _screeningList = new ObservableCollection<ScreeningInfo>();
        private ScreeningInfo _selectedScreening;

        private string _selectedItem;
        
        public ICommand ScreeningSelectedCommand
        {
            get
            {
                if (_screeningSelectedCommand == null)
                {
                    _screeningSelectedCommand = new RelayCommand<ScreeningInfo>(param => HandleScreeningSelected(param));
                }
                return _screeningSelectedCommand;
            }
        }

        private async Task HandleScreeningSelected(ScreeningInfo selectedScreening)
        {
            if (selectedScreening != null)
            {

                if(user==null)
                {
                    MessageBox.Show("Please sign in to buy ticket");
                    return;
                }
                BuyTicketView buyTicketView = new BuyTicketView();
                BuyTicketViewModel buyTicketViewModel = new BuyTicketViewModel(movie, selectedScreening, user);
                buyTicketView.DataContext = buyTicketViewModel;
                buyTicketView.ShowDialog();
                movie = await movieService.GetMovieById(movie.Id);
                this._screeningList.Clear();
                List<Screening> screeningList = movieService.GetScreenings(movie);
                for (int i = 0; i < screeningList.Count; i++)
                {
                    ScreeningInfo screeningInfo = new ScreeningInfo(screeningList[i], (TimeSpan)movie.Duration);
                    this._screeningList.Add(screeningInfo);
                }
            }
        }

        public ObservableCollection<ScreeningInfo> ScreeningList
        {
            get => _screeningList;
            set
            {
                _screeningList = value;
                OnPropertyChanged(nameof(ScreeningList));
            }
        }
        public ScreeningInfo SelectedScreening
        {
            get => _selectedScreening;
            set
            {
                _selectedScreening = value;
                OnPropertyChanged(nameof(SelectedScreening));
            }
        }
        
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public List<string> DirectorList
        {
            get => _directorList;
        }

        public List<string> ActorList
        {
            get => _actorList;
        }




        public List<Genre> GenreList
        {
            get => (List<Genre>)movie.Genres;
        }

        public ICommand BuyButton
        {
            get
            {
                if (_buyButton == null)
                {
                    _buyButton = new RelayCommand(param => HandleBuyButton());
                }
                return _buyButton;
            }
        }

        public ICommand PlayButton
        {
            get
            {
                if (_playButton == null)
                {
                    _playButton = new RelayCommand(param => HandlePlayButton());
                }
                return _playButton;
            }
        }

        private void HandleBuyButton()
        {
        }

        private void HandlePlayButton()
        {
            //Do nothing
        }



        public string Name
        {
            get => movie.Name;
            set
            {
                movie.Name = value;
                OnPropertyChanged(nameof(Name));
            }

        }
        public int PublishedYear
        {
            get => (int)movie.PublishedYear;
            set
            {
                movie.PublishedYear = value;
                OnPropertyChanged(nameof(PublishedYear));
            }
        }
        public TimeSpan Duration
        {
            get => (TimeSpan)movie.Duration;
            set
            {
                movie.Duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
        public float ImdbRating
        {
            get => (float)movie.ImdbRating;
            set
            {
                movie.ImdbRating = value;
                OnPropertyChanged(nameof(ImdbRating));
            }
        }

        public MovieCertification Certification
        {
            get => movie.Certification;
            set
            {
                movie.Certification = value;
                OnPropertyChanged(nameof(Certification));
            }
        }

        public string PosterImg
        {
            get => movie.PosterImg;
            set
            {
                movie.PosterImg = value;
                OnPropertyChanged(nameof(PosterImg));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
