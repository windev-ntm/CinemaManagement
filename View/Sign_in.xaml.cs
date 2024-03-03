using System.Windows;
using System.Windows.Controls;

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for Sign_in.xaml
    /// </summary>
    public partial class Sign_in : Window
    {
        public Sign_in()
        {
            InitializeComponent();

            DataContext = new CinemaManagement.ViewModel.SignInViewModel();

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
