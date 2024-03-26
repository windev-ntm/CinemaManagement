using CinemaManagement.Models;
using CinemaManagement.Services;
using CinemaManagement.View;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel
{

    public class SignInViewModel : INotifyPropertyChanged
    {
        private UserService userService;
        private User user;

        private ICommand _signInCommand, _signUpCommand;

        public string logoPath { get; set; }


        public SignInViewModel()
        {
            user = new User();
            userService = new UserService();
        }

        public ICommand SignUpCommand
        {
            get
            {
                if (_signUpCommand == null)
                {
                    _signUpCommand = new RelayCommand(param => HandleSignUpButton());
                }
                return _signUpCommand;
            }
        }


        public ICommand SignInCommand
        {
            get
            {
                if (_signInCommand == null)
                {
                    _signInCommand = new RelayCommand(param => HandleSignInButton());
                }
                return _signInCommand;
            }
        }

        private async Task HandleSignInButton()
        {
            user = await userService.SignIn(user.Username, user.Password);
            if(user != null)
            {
                Home home = new Home();
                HomeViewModel homeViewModel = new HomeViewModel();
                home.DataContext = homeViewModel;


                WindowHome windowHome = new WindowHome(home);

                if (home != null)
                {
                    windowHome.Show();
                    Application.Current.MainWindow.Close(); // Đối với cửa sổ chính của ứng dụng
                    Application.Current.MainWindow = windowHome;
                }
            }
        }

        private void HandleSignUpButton()
        {
            SignUp signUp = new SignUp();

            signUp.Show();

            Application.Current.MainWindow.Close(); // Đối với cửa sổ chính của ứng dụng

            Application.Current.MainWindow = signUp;
        }


        public string Username
        {
            get => user.Username;
            set
            {
                user.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
