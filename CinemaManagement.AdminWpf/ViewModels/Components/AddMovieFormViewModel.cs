using CinemaManagement.AdminWpf.Services;
using CinemaManagement.AdminWpf.ViewModels.Pages;
using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.ViewModels.Components
{
    public partial class AddMovieFormViewModel : ObservableValidator, INavigationAware
    {
        private readonly MovieService _movieService;
        private readonly IViewProvider _viewProvider;
        private readonly INavigationService _navigationService;

        public AddMovieFormViewModel(MovieService movieService,
            IViewProvider viewProvider,
            INavigationService navigationService
        )
        {
            _movieService = movieService;
            _viewProvider = viewProvider;
            _navigationService = navigationService;
        }

        public event Action? OnCreatedSuccessful;
        public event Action? OnCreatedFailed;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(1)]
        private string _name = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Range(0, int.MaxValue)]
        private int _durationMinutes;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Range(0, int.MaxValue)]
        private int _publishedYear;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Range(0, 10)]
        private float _imdbRating;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [CustomValidation(typeof(AddMovieFormViewModel), nameof(ValidateUrl))]
        private string _posterImg = string.Empty;

        [ObservableProperty]
        private string _urlToLoad = string.Empty;

        public List<MovieCertification> Certifications { get; private set; } = [];

        public List<Person> CrewMembers { get; private set; } = [];

        private bool CanCreate
        {
            get { ValidateAllProperties(); return !HasErrors; }
        }

        [RelayCommand]
        private void Create()
        {
            bool res = true;

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

        public static ValidationResult? ValidateUrl(string url, ValidationContext context)
        {
            bool result = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result) return ValidationResult.Success;

            return new("Invalid URL format.");
        }

        public void OnNavigatedTo()
        {
            var parentViewModel = _viewProvider.GetViewModel<MoviesViewModel>();

            parentViewModel.RegisterEvents(this);
        }

        public void OnNavigatedFrom() { }
    }
}
