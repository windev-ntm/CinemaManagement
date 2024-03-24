using CinemaManagement.AdminWpf.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Pages
{
    /// <summary>
    /// Interaction logic for GenresPage.xaml
    /// </summary>
    public partial class GenresPage : INavigableView<GenresViewModel>
    {
        public GenresViewModel ViewModel { get => (GenresViewModel)DataContext; }

        public GenresPage(GenresViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
