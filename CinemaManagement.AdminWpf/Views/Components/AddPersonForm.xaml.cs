using CinemaManagement.AdminWpf.ViewModels.Components;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Components
{
    /// <summary>
    /// Interaction logic for AddPersonForm.xaml
    /// </summary>
    public partial class AddPersonForm : ContentDialog
    {
        public AddPersonFormViewModel ViewModel { get => (AddPersonFormViewModel)DataContext; }

        public AddPersonForm(AddPersonFormViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnButtonClick(ContentDialogButton button)
        {
            if (button == ContentDialogButton.Primary)
            {
                if (!ViewModel.CreateCommand.CanExecute(null)) return;
                ViewModel.CreateCommand.Execute(null);
            }

            base.OnButtonClick(button);
        }
    }
}
