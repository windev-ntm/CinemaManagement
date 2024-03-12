using CinemaManagement.Entity;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel
{

    public class SignInViewModel : INotifyPropertyChanged
    {
        private UserEntity userEntity;

        private ICommand _signInCommand, _signUpCommand;

        public string logoPath { get; set; }


        public SignInViewModel()
        {
            userEntity = new UserEntity();
            logoPath = Directory.GetCurrentDirectory() + "\\logo.png";
            Console.WriteLine(logoPath);
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
            MessageBox.Show("Username:" + Username + "\n" + "Password: " + Password);
        }

        private void HandleSignUpButton()
        {
            //var signUp = new View.Sign_up();

        }


        public string Username
        {
            get { return userEntity.getUsername(); }
            set
            {
                userEntity.setUsername(value);
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get { return userEntity.getPassword(); }
            set
            {
                userEntity.setPassword(value);
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
