using CinemaManagement.AdminWpf.ViewModels.Components;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Components
{
    /// <summary>
    /// Interaction logic for AddPersonToMovieForm.xaml
    /// </summary>
    public partial class AddPersonToMovieForm : ContentDialog
    {
        public AddPersonToMovieFormViewModel ViewModel { get => (AddPersonToMovieFormViewModel)DataContext; }

        public AddPersonToMovieForm(AddPersonToMovieFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnButtonClick(ContentDialogButton button)
        {
            if (button == ContentDialogButton.Primary)
            {
                ViewModel.AddToMovieCommand.Execute(null);
                if (ViewModel.HasErrors) return;
            }

            base.OnButtonClick(button);
        }
    }
}
