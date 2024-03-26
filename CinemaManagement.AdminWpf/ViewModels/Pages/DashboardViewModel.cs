using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject, INavigationAware
    {
        private readonly MovieService _movieService;
        private readonly UserService _userService;
        private readonly TicketService _ticketService;

        private bool _isInitialized;



        public DashboardViewModel(
            MovieService movieService,
            UserService userService,
            TicketService ticketService
        )
        {
            _movieService = movieService;
            _userService = userService;
            _ticketService = ticketService;
        }

        [ObservableProperty]
        private int _numberOfMovies;

        [ObservableProperty]
        private int _numberOfUsers;

        [ObservableProperty]
        private int _totalRevenue;

        [ObservableProperty]
        private int _totalTicketsSold;

        public void OnNavigatedFrom()
        {

        }

        public async void OnNavigatedTo()
        {
            await Task.WhenAll(
                LoadMoviesData(),
                LoadUsersData(),
                LoadProfitsData()
            );
        }

        private async Task LoadMoviesData()
        {
            NumberOfMovies = await Task.Run(() => _movieService.GetNumberOfMovies());
        }

        private async Task LoadUsersData()
        {
            NumberOfUsers = await Task.Run(() => _userService.GetNumberOfUsers());
        }

        private async Task LoadProfitsData()
        {
            var task1 = Task.Run(() => _ticketService.GetTotalTicketsSold());
            var task2 = Task.Run(() => _ticketService.GetTotalValueOfTickets());

            var res = await Task.WhenAll(task1, task2);
            TotalTicketsSold = res[0];
            TotalRevenue = res[1];
        }
    }
}
