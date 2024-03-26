using CinemaManagement.AdminWpf.ViewModels.Pages;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.ViewModels.Components
{
    public partial class AddPersonToMovieFormViewModel : ObservableValidator, INavigationAware
    {
        private readonly MoviePersonService _moviePersonService;
        private readonly IServiceProvider _serviceProvider;
        private int _personId;
        private int _movieId;

        public AddPersonToMovieFormViewModel(
            MoviePersonService moviePersonService,
            IServiceProvider serviceProvider
        )
        {
            _moviePersonService = moviePersonService;
            _serviceProvider = serviceProvider;
        }

        public event Action? OnAddedSuccessful;
        public event Action? OnAddedFailed;

        public bool CanAddToMovie
        {
            get
            {
                ValidateAllProperties();
                return !HasErrors;
            }
        }

        [ObservableProperty]
        [Required]
        [MinLength(1)]
        private string _role = string.Empty;

        private string _personName = string.Empty;
        public string PersonName { get => _personName; }


        [RelayCommand(CanExecute = nameof(CanAddToMovie))]
        private async Task AddToMovie()
        {
            var res = await Task.Run(() =>
                _moviePersonService.AddPersonToMovie(_personId, _movieId, Role));

            if (res)
                OnAddedSuccessful?.Invoke();
            else
                OnAddedFailed?.Invoke();
        }

        public void Initialize(int personId, int movieId, string personName)
        {
            _personId = personId;
            _movieId = movieId;
            _personName = personName;
        }

        public void OnNavigatedTo()
        {
            var rootViewModel = _serviceProvider.GetRequiredService<MoviesViewModel>();
            var parentViewModel = rootViewModel.AddMovieFormViewModel;

            if (parentViewModel is null)
                throw new InvalidOperationException("Parent view model doesn't exist");

        }

        public void OnNavigatedFrom()
        {

        }
    }
}
