using CinemaManagement.Models;
using CinemaManagement.Services;
using CinemaManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for UpdateAccountInformationWindow.xaml
    /// </summary>
    public partial class UpdateAccountInformationWindow : Window
    {
        UpdateAccountViewModel updateAccountViewModel;
        public UpdateAccountInformationWindow(int v)
        {
            InitializeComponent();
            updateAccountViewModel = new ViewModel.UpdateAccountViewModel(1);
            DataContext = updateAccountViewModel;
            //TicketListView.ItemsSource = updateAccountViewModel.Tickets;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext).password = PasswordPasswordBox.Password;
            }

        }

        private void TicketListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Ticket selectedItem = TicketListView.SelectedItem as Ticket;

            if (selectedItem != null)
            {
                selectedItem.Screening = TicketService.GetScreeningOfTicket(selectedItem);
                // Create a new instance of the desired window
                var newWindow = new TicketDetailInfo(selectedItem);

                // Show the new window
                newWindow.Show();
            }
        }
    }
}
