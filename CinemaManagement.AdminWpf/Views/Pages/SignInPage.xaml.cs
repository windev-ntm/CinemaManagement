using CinemaManagement.AdminWpf.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Pages
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : INavigableView<SignInViewModel>
    {
        public SignInViewModel ViewModel { get => (SignInViewModel)DataContext; }

        public SignInPage(SignInViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ViewModel != null)
            { ViewModel.Password = ((PasswordBox)sender).Password; }
        }
    }
}
