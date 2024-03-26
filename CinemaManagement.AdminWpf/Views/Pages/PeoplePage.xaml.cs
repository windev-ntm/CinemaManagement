using CinemaManagement.AdminWpf.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Pages
{
    /// <summary>
    /// Interaction logic for PeoplePage.xaml
    /// </summary>
    public partial class PeoplePage : INavigableView<PeopleViewModel>
    {
        public PeoplePage(PeopleViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public PeopleViewModel ViewModel { get => (PeopleViewModel)DataContext; }
    }
}
