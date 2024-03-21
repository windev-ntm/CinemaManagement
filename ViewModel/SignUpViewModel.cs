using CinemaManagement.Models;
using CinemaManagement.Services;
using CinemaManagement.View;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
namespace CinemaManagement.ViewModel
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private UserService userService;
        private User user;
        private ICommand _signInCommand, _signUpCommand;


                public SignUpViewModel()
        {
            user = new User();
            userService = new UserService();
            Gender = null;
            GenderOptions = new List<string> { "Male", "Female", "Other" };
        }


        private List<string> _genderOptions;

        public List<string> GenderOptions
        {
            get { return _genderOptions; }
            set
            {
                if (_genderOptions != value)
                {
                    _genderOptions = value;
                    OnPropertyChanged(nameof(GenderOptions));
                }
            }
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
        public DateTime? BirthDate
        {
            get => user.BirthDate;
            set
            {
                Debug.WriteLine(value.ToString());
                user.BirthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        public string Gender
        {
            get => user.Gender?.ToString(); // Chuyển đổi ComboBoxItem sang string nếu cần
            set
            {
                user.Gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        public string TextButton
        {
            get => "Sign Up";
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
            Sign_in signIn = new Sign_in();

            signIn.Show();

            Application.Current.MainWindow.Close(); // Đối với cửa sổ chính của ứng dụng

            Application.Current.MainWindow = signIn;
        }
        private void HandleSignUpButton()
        {
            userService.CreateUser(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
