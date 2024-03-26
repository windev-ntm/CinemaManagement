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
        public Action CloseAction { get; set; }
        private Window window;

        private string _username;
        private string _password;

        public SignInViewModel(Window window)
        {
            this.window = window;
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

        private void HandleSignInButton()
        {
            User user = userService.SignIn(_username, _password);
            if(user != null)
            {
                this.window.Close();
            }
        }

        private void HandleSignUpButton()
        {
            this.window.Close();

            SignUp signUp = new SignUp();

            signUp.Show();

        }


        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
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
