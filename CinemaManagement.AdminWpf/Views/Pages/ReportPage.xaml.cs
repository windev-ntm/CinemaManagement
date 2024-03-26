using CinemaManagement.AdminWpf.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Pages
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : INavigableView<ReportViewModel>
    {
        public ReportPage(ReportViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public ReportViewModel ViewModel { get => (ReportViewModel)DataContext; }
    }
}
