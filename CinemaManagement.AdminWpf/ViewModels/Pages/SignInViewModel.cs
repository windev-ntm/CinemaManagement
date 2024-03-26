using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace CinemaManagement.AdminWpf.ViewModels.Pages
{
    public partial class SignInViewModel : ObservableValidator
    {
        private readonly AdminService _adminService;
        private readonly ISnackbarService _SnackbarService;

        public SignInViewModel(
            AdminService adminService,
            ISnackbarService snackbarService
        )
        {
            _adminService = adminService;
            _SnackbarService = snackbarService;
        }

        public event Action? OnSignedIn;

        [ObservableProperty]
        [MinLength(1)]
        private string _username = string.Empty;

        [ObservableProperty]
        [MinLength(1)]
        private string _password = string.Empty;

        public bool CanSignIn
        {
            get
            {
                ValidateAllProperties();
                return !HasErrors;
            }
        }

        [RelayCommand]
        private async Task SignIn()
        {
            if (!CanSignIn) { return; }

            bool res = await Task.Run(() => _adminService.SignIn(
                Username,
                Password
            ));

            if (res)
            {
                OnSignedIn?.Invoke();
            }
            else
            {
                _SnackbarService.Show("Sign in", "Invalid username or password!");
            }
        }
    }
}
