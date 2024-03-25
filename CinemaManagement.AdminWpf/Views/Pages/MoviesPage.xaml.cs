using CinemaManagement.AdminWpf.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Pages
{
    /// <summary>
    /// Interaction logic for MoviesPage.xaml
    /// </summary>
    public partial class MoviesPage : INavigableView<MoviesViewModel>
    {
        public MoviesPage(MoviesViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public MoviesViewModel ViewModel { get => (MoviesViewModel)DataContext; }
    }
}
