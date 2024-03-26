using CinemaManagement.ViewModel;
using System.Windows;
using System.Windows.Controls;
namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for Sign_in.xaml
    /// </summary>
    public partial class Sign_in : Window
    {
        public SignInViewModel signInViewModel { get; set; }
        public Sign_in()
        {
            InitializeComponent();

            signInViewModel = new SignInViewModel(this);
            DataContext = signInViewModel;
            if (signInViewModel.CloseAction == null)
                signInViewModel.CloseAction = new System.Action(this.Close);

        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

    }


}
