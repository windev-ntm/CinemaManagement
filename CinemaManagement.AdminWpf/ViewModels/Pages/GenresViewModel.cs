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
        private async Task OpenCreateDialog()
        {
            var dialogView = _viewProvider
                .GetView<AddGenreFormViewModel>(out var dialogViewModel);

            dialogViewModel.OnCreatedSuccessful += async () =>
            {
                _snackbarService.Show("Genres ", "New genre added", ControlAppearance.Success);
                await LoadGenres();
            };
            dialogViewModel.OnCreatedFailed += async () =>
            {
                _snackbarService.Show("Genres", "Failed to add new genre", ControlAppearance.Danger);
                await LoadGenres();
            };

            await _contentDialogService.ShowAsync(dialogView);
        }

        [RelayCommand]
        private async Task OpenEditDialog(Genre genre)
        {
            var dialogView = _viewProvider
                .GetView<EditGenreFormViewModel>(out var dialogViewModel);

            dialogViewModel.SetData(genre);
            dialogViewModel.OnCreatedSuccessful += async () =>
            {
                _snackbarService.Show("Genres ", "Genre updated", ControlAppearance.Success);
                await LoadGenres();
            };
            dialogViewModel.OnCreatedFailed += async () =>
            {
                _snackbarService.Show("Genres", "Failed to update genre", ControlAppearance.Danger);
                await LoadGenres();
            };

            await _contentDialogService.ShowAsync(dialogView);
        }

        [RelayCommand]
        private async Task OpenDeleteDialog(Genre genre)
        {
            var res = await _contentDialogService.ShowSimpleDialogAsync(
                new SimpleContentDialogCreateOptions
                {
                    Title = "Delete Genre",
                    Content = $"Are you sure you want to delete {genre.Name}?",
                    PrimaryButtonText = "Confirm",
                    CloseButtonText = "Cancel"
                }
            );

            if (res == ContentDialogResult.Primary)
            {
                string? error = null;
                var deleteRes = await Task.Run(() =>
                {
                    try
                    {
                        return _genreService.DeleteGenre(genre.Id);
                    }
                    catch (Exception e)
                    {
                        error = e.Message;
                        return false;
                    }
                });

                error ??= "Failed to delete genre";

                if (deleteRes)
                {
                    _snackbarService.Show("Genres", "Genre deleted", ControlAppearance.Success);
                    await LoadGenres();
                }
                else
                {
                    _snackbarService.Show("Genres", error, ControlAppearance.Danger);
                }

            }
        }
    }
}
