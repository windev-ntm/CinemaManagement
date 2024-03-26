using CinemaManagement.AdminWpf.ViewModels.Components;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Components
{
    /// <summary>
    /// Interaction logic for EditPersonForm.xaml
    /// </summary>
    public partial class EditPersonForm : ContentDialog
    {
        public EditPersonFormViewModel ViewModel { get => (EditPersonFormViewModel)DataContext; }

        public EditPersonForm(EditPersonFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnButtonClick(ContentDialogButton button)
        {
            if (button == ContentDialogButton.Primary)
            {
                if (!ViewModel.EditCommand.CanExecute(null)) return;
                ViewModel.EditCommand.Execute(null);
            }

            base.OnButtonClick(button);
        }
    }
}
