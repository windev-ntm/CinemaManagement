using System.Windows;
using System.Windows.Controls;

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
            DataContext = new CinemaManagement.ViewModel.SignUpViewModel(this);
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
