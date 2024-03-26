using CinemaManagement.AdminWpf.Services;
using CinemaManagement.AdminWpf.ViewModels.Pages;
using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace CinemaManagement.AdminWpf.ViewModels.Components
{
    public partial class AddMovieFormViewModel : ObservableValidator, INavigationAware
    {
        private readonly MovieService _movieService;
        private readonly GenreService _genreService;
        private readonly IViewProvider _viewProvider;
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IContentDialogService _contentDialogService;
        private readonly MoviePersonService _moviePersonService;

        private Person? _selectedPerson;

        public AddMovieFormViewModel(
            MovieService movieService,
            GenreService genreService,
            IViewProvider viewProvider,
            INavigationService navigationService,
            IServiceProvider serviceProvider,
            IContentDialogService contentDialogService,
            MoviePersonService moviePersonService
        )
        {
            _movieService = movieService;
            _genreService = genreService;
            _viewProvider = viewProvider;
            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
            _contentDialogService = contentDialogService;
            _moviePersonService = moviePersonService;
        }


        public event Action? OnCreatedSuccessful;
        public event Action? OnCreatedFailed;

        #region Properties

        public ObservableCollection<MovieCertification> Certifications { get; } = [];
        public ObservableCollection<PersonInMovie> MoviePeople { get; } = [];
        public ObservableCollection<ExtendedGenre> Genres { get; } = [];

        [ObservableProperty]
        [Required]
        private MovieCertification _selectedCertification;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(1)]
        private string _name = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Range(1, int.MaxValue)]
        private int _durationMinutes;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Range(0, int.MaxValue)]
        private int _publishedYear;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Range(0.1, 10)]
        private float _imdbRating;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [CustomValidation(typeof(AddMovieFormViewModel), nameof(ValidateUrl))]
        private string _posterImg = string.Empty;

        [ObservableProperty]
        private string _urlToLoad = string.Empty;

        public bool CanCreate
        {
            get
            {
                ValidateAllProperties();
                Debug.WriteLine($"HasErrors: {HasErrors}");
                Debug.WriteLine($"SelectedGenres: {SelectedGenres.Count}");
                return !HasErrors && SelectedGenres.Count > 0;
            }
        }

        [ObservableProperty]
        private bool _isSelectingGenres = true;

        public List<Genre> SelectedGenres
        {
            get
            {
                return Genres
                .Where(g => g.IsSelected)
                .Select(g => g.Value)
                .ToList();
            }
        }

        #endregion Properties

        #region Commands

        [RelayCommand]
        private void StartSelecting()
        {
            IsSelectingGenres = true;
        }

        [RelayCommand]
        private void StopSelecting()
        {
            IsSelectingGenres = false;
            OnPropertyChanged(nameof(SelectedGenres));
        }


        [RelayCommand]
        private async Task Create()
        {
            if (!CanCreate)
            {
                await _contentDialogService.ShowAlertAsync("Error", "Please correctly fill in all fields", "Close");
                return;
            }

            var movie = new Movie
            {
                Name = Name,
                Duration = TimeSpan.FromMinutes(DurationMinutes),
                PosterImg = PosterImg,
                PublishedYear = PublishedYear,
                ImdbRating = ImdbRating,
                CertificationId = SelectedCertification.Id
            };


            bool res = await Task.Run(() => _movieService.AddMovie(movie, SelectedGenres));

            if (res)
                OnCreatedSuccessful?.Invoke();
            else
                OnCreatedFailed?.Invoke();
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationService.GoBack();
        }

        [RelayCommand]
        private void LoadPosterImage()
        {
            UrlToLoad = PosterImg;
        }

        [RelayCommand]
        private async Task OpenDialog(object content)
        {
            ContentDialogResult result = await _contentDialogService.ShowSimpleDialogAsync(
                new SimpleContentDialogCreateOptions()
                {
                    Title = "Save your work?",
                    Content = content,
                    PrimaryButtonText = "Save",
                    SecondaryButtonText = "Don't Save",
                    CloseButtonText = "Cancel",
                }
            );
        }

        #endregion Commands

        public static ValidationResult? ValidateUrl(string url, ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(url)) return ValidationResult.Success;

            bool result = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result) return ValidationResult.Success;

            return new("Invalid URL format.");
        }

        public void Configure(AddPersonToMovieFormViewModel childViewModel)
        {
            throw new NotImplementedException();
        }

        public async void OnNavigatedTo()
        {
            var parentViewModel = _viewProvider.GetViewModel<MoviesViewModel>();

            parentViewModel.RegisterEvents(this);

            await Task.WhenAll(
                LoadAvailableGenres(),
                LoadCertifications()
            );
        }

        public void OnNavigatedFrom() { }

        private async Task LoadAvailableGenres()
        {
            var res = await Task.Run(() => _genreService.GetAllGenres());
            Genres.Clear();
            foreach (var genre in res)
            {
                Genres.Add(new(genre, false));
            }
        }

        private async Task LoadCertifications()
        {
            var res = await Task.Run(() => _movieService.GetAllCertifications());
            Certifications.Clear();
            foreach (var certification in res)
            {
                Certifications.Add(certification);
            }
        }

        public record ExtendedGenre(Genre Value, bool IsSelected);
    }
}
