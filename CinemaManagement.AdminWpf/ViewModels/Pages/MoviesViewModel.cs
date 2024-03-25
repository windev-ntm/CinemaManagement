using CinemaManagement.AdminWpf.Services;
using CinemaManagement.AdminWpf.ViewModels.Components;
using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace CinemaManagement.AdminWpf.ViewModels.Pages
{
    public partial class MoviesViewModel : ObservableObject, INavigationAware
    {
        private const int PageSize = 5;

        private readonly MovieService _movieService;
        private readonly IViewProvider _viewProvider;
        private readonly ISnackbarService _snackbarService;
        private readonly IContentDialogService _contentDialogService;
        private readonly INavigationService _navigationService;
        private bool _isInitialized = false;

        public MoviesViewModel(
            MovieService movieService,
            IViewProvider viewProvider,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService,
            INavigationService navigationService
        )
        {
            _movieService = movieService;
            _viewProvider = viewProvider;
            _snackbarService = snackbarService;
            _contentDialogService = contentDialogService;
            _navigationService = navigationService;
        }

        private readonly ObservableCollection<Movie> _movies = [];
        public ObservableCollection<Movie> Movies
        {
            get => _movies;
        }

        public int CorrectCurrentPage { get => TotalPages == 0 ? 0 : CurrentPage; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoadNextPageCommand))]
        [NotifyCanExecuteChangedFor(nameof(LoadPreviousPageCommand))]
        [NotifyCanExecuteChangedFor(nameof(LoadFirstPageCommand))]
        [NotifyCanExecuteChangedFor(nameof(LoadLastPageCommand))]
        [NotifyPropertyChangedFor(nameof(CorrectCurrentPage))]
        private int _currentPage = 1;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoadNextPageCommand))]
        [NotifyCanExecuteChangedFor(nameof(LoadLastPageCommand))]
        [NotifyPropertyChangedFor(nameof(CorrectCurrentPage))]
        private int _totalPages;

        [ObservableProperty]
        private string _searchText = string.Empty;
        async partial void OnSearchTextChanged(string value)
        {
            Debug.WriteLine(value);
            CurrentPage = 1;
            await LoadMovies();
        }

        [ObservableProperty]
        private bool _isLoading = false;

        private bool CanLoadNextPage => CurrentPage < TotalPages;
        private bool CanLoadPreviousPage => CurrentPage > 1;


        [RelayCommand(CanExecute = nameof(CanLoadNextPage))]
        private async Task LoadNextPage()
        {
            CurrentPage += 1;
            await LoadMovies();
        }

        [RelayCommand(CanExecute = nameof(CanLoadPreviousPage))]
        private async Task LoadPreviousPage()
        {
            CurrentPage -= 1;
            await LoadMovies();
        }
        [RelayCommand(CanExecute = nameof(CanLoadNextPage))]
        private async Task LoadLastPage()
        {
            CurrentPage = TotalPages;
            await LoadMovies();
        }

        [RelayCommand(CanExecute = nameof(CanLoadPreviousPage))]
        private async Task LoadFirstPage()
        {
            CurrentPage = 1;
            await LoadMovies();
        }

        [RelayCommand]
        private void NavigateForward(Type viewType)
        {
            _navigationService.NavigateWithHierarchy(viewType);
        }

        public void OnNavigatedFrom() { }

        public async void OnNavigatedTo()
        {
            if (_isInitialized) return;

            await LoadMovies();
            _isInitialized = true;
        }

        public void RegisterEvents(AddMovieFormViewModel childViewModel)
        {
            childViewModel.OnCreatedSuccessful += async () =>
            {
                _snackbarService.Show("Genres ", "New genre added", ControlAppearance.Success);
                await LoadMovies();
            };
            childViewModel.OnCreatedFailed += async () =>
            {
                _snackbarService.Show("Genres", "Failed to add new genre", ControlAppearance.Danger);
                await LoadMovies();
            };
        }

        private async Task LoadMovies()
        {
            IsLoading = true;

            var task = Task.Run(() => _movieService.GetTotalPages(PageSize, SearchText));

            var res = await Task.Run(() => _movieService.GetMovies(CurrentPage, PageSize, SearchText));
            Movies.Clear();
            foreach (var movie in res)
            {
                Movies.Add(movie);
            }

            TotalPages = await task;

            IsLoading = false;
        }
    }
}
