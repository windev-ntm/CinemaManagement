using CinemaManagement.AdminWpf.Helpers;
using CinemaManagement.AdminWpf.Services;
using CinemaManagement.AdminWpf.ViewModels.Components;
using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace CinemaManagement.AdminWpf.ViewModels.Pages
{
    public partial class GenresViewModel : ObservableObject, INavigationAware
    {
        private readonly GenreService _genreService;

        private readonly IContentDialogService _contentDialogService;

        private readonly ISnackbarService _snackbarService;

        private readonly IViewProvider _viewProvider;

        private bool _isInitialized = false;

        [ObservableProperty]
        private string _testText = string.Empty;


        private readonly ObservableCollection<Genre> _genres = [];
        public ObservableCollection<Genre> Genres
        {
            get => _genres;
        }

        public GenresViewModel(
            GenreService genreService,
            IContentDialogService contentDialogService,
            ISnackbarService snackbarService,
            IViewProvider viewProvider
        )
        {
            _genreService = genreService;
            _contentDialogService = contentDialogService;
            _snackbarService = snackbarService;
            _viewProvider = viewProvider;
        }

        public void OnNavigatedFrom() { }

        public async void OnNavigatedTo()
        {
            if (_isInitialized) return;

            await LoadGenres();
            _isInitialized = true;
        }

        private async Task LoadGenres()
        {
            var res = await Task.Run(() => _genreService.GetAllGenres());
            _genres.Clear();
            foreach (var genre in res)
            {
                _genres.Add(genre);
            }
        }

        [RelayCommand]
        private async Task ShowCreateDialog()
        {
            var cancelToken = new CancellationTokenSource();

            var genreFormVM = _viewProvider.GetViewModel<AddGenreFormViewModel>()!;
            genreFormVM.OnCreatedSuccessful += async () =>
            {
                _snackbarService.Show("Genres ", "New genre added", ControlAppearance.Success);
                await LoadGenres();
            };
            genreFormVM.OnCreatedFailed += async () =>
            {
                _snackbarService.Show("Genres", "Failed to add new genre", ControlAppearance.Caution);
                await LoadGenres();
            };

            await _contentDialogService.ShowAsync(_viewProvider.GetViewAndUpdateContext(genreFormVM)!);
        }
    }
}
