using CinemaManagement.AdminWpf.ViewModels.Components;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Components
{
    /// <summary>
    /// Interaction logic for AddMovieForm.xaml
    /// </summary>
    public partial class AddMovieForm : INavigableView<AddMovieFormViewModel>
    {
        public AddMovieForm(AddMovieFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public AddMovieFormViewModel ViewModel
        {
            get => (AddMovieFormViewModel)DataContext;
        }
    }
}
