using CinemaManagement.AdminWpf.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get => (DashboardViewModel)DataContext; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}
