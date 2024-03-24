using CinemaManagement.AdminWpf.ViewModels.Components;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Components
{
    /// <summary>
    /// Interaction logic for EditGenreForm.xaml
    /// </summary>
    public partial class GenreForm : ContentDialog
    {
        public AddGenreFormViewModel ViewModel { get => (AddGenreFormViewModel)DataContext; }

        public GenreForm(AddGenreFormViewModel viewModel)
        {

            DataContext = ViewModel;
            InitializeComponent();
        }

        private void ContentDialog_ButtonClicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (args.Button is ContentDialogButton.Primary)
            {
                ViewModel.CreateGenreCommand.Execute(null);
            }
            return;
        }
    }
}
