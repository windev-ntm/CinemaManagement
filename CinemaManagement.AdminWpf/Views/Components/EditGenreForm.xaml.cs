using CinemaManagement.AdminWpf.ViewModels.Components;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Components
{
    /// <summary>
    /// Interaction logic for EditGenreForm.xaml
    /// </summary>
    public partial class EditGenreForm : ContentDialog
    {
        public EditGenreFormViewModel ViewModel
        {
            get => (EditGenreFormViewModel)DataContext;
        }

        public EditGenreForm(EditGenreFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnButtonClick(ContentDialogButton button)
        {
            if (button == ContentDialogButton.Primary)
            {
                ViewModel.UpdateGenreCommand.Execute(null);
                if (ViewModel.HasErrors) return;
            }

            base.OnButtonClick(button);
        }
    }
}
