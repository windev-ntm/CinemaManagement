using CinemaManagement.AdminWpf.Helpers;
using CinemaManagement.AdminWpf.ViewModels.Components;
using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace CinemaManagement.AdminWpf.ViewModels.Pages
{
    public partial class PeopleViewModel : ObservableObject, INavigationAware
    {
        private const int PageSize = 15;

        private readonly MoviePersonService _moviePersonService;
        private readonly INavigationService _navigationService;
        private readonly ISnackbarService _snackbarService;
        private readonly IContentDialogService _contentDialogService;
        private readonly IServiceProvider _serviceProvider;
        private bool _isInitialized = false;

        public PeopleViewModel(
            MoviePersonService moviePersonService,
            INavigationService navigationService,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService,
            IServiceProvider serviceProvider
        )
        {
            _moviePersonService = moviePersonService;
            _navigationService = navigationService;
            _snackbarService = snackbarService;
            _contentDialogService = contentDialogService;
            _serviceProvider = serviceProvider;
        }

        public ObservableCollection<Person> People { get; private set; } = [];

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
            await LoadData();
            CurrentPage = 1;
        }

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private Person? _selectedPerson;


        private bool CanLoadNextPage => CurrentPage < TotalPages;
        private bool CanLoadPreviousPage => CurrentPage > 1;


        [RelayCommand(CanExecute = nameof(CanLoadNextPage))]
        private async Task LoadNextPage()
        {
            CurrentPage += 1;
            await LoadData();
        }

        [RelayCommand(CanExecute = nameof(CanLoadPreviousPage))]
        private async Task LoadPreviousPage()
        {
            CurrentPage -= 1;
            await LoadData();
        }
        [RelayCommand(CanExecute = nameof(CanLoadNextPage))]
        private async Task LoadLastPage()
        {
            CurrentPage = TotalPages;
            await LoadData();
        }

        [RelayCommand(CanExecute = nameof(CanLoadPreviousPage))]
        private async Task LoadFirstPage()
        {
            CurrentPage = 1;
            await LoadData();
        }

        [RelayCommand]
        private void OpenAddDialog(Type viewType)
        {
            var view = _serviceProvider.GetRequiredService(viewType);
            _contentDialogService.ShowAsync(view);
        }

        [RelayCommand]
        private void OpenEditDialog(Type viewType)
        {
            // Nothing to edit
            if (SelectedPerson is null) return;

            var view = _serviceProvider.GetRequiredService(viewType);
            _contentDialogService.ShowAsync(view);
        }


        public void OnNavigatedFrom() { }

        public async void OnNavigatedTo()
        {
            if (_isInitialized) return;

            await LoadData();
            _isInitialized = true;
        }

        public void RegisterEvents(AddPersonFormViewModel childViewModel)
        {
            childViewModel.OnCreatedSuccessful += async () =>
            {
                _snackbarService.Show("Success", "New person added", ControlAppearance.Success);
                await LoadData();
            };
            childViewModel.OnCreatedFailed += async () =>
            {
                _snackbarService.Show("Failed", "Unable to add new person", ControlAppearance.Danger);
                await LoadData();
            };
        }

        public void Configure(EditPersonFormViewModel childViewModel)
        {
            childViewModel.Initialize(SelectedPerson!);

            childViewModel.OnCreatedSuccessful += async () =>
            {
                _snackbarService.Show("Success", "Changes were saved", ControlAppearance.Success);
                await LoadData();
            };
            childViewModel.OnCreatedFailed += async () =>
            {
                _snackbarService.Show("Failed", "Unable to save changes", ControlAppearance.Danger);
                await LoadData();
            };
        }

        private async Task LoadData()
        {
            IsLoading = true;

            var task = Task.Run(() => _moviePersonService.GetTotalPages(PageSize, SearchText));

            var res = await Task.Run(() =>
                _moviePersonService.GetAllPeople(CurrentPage, PageSize, SearchText)
            );
            People.Clear();
            foreach (var person in res)
            {
                People.Add(person);
            }

            TotalPages = await task;

            IsLoading = false;
        }
    }
}
