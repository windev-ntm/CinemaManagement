using CinemaManagement.Models;
using CinemaManagement.Services;
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
    /// Interaction logic for TicketDetailInfo.xaml
    /// </summary>
    public partial class TicketDetailInfo : Window
    {
        public TicketDetailInfo(Ticket ticket)
        {
            InitializeComponent();
            DataContext = new CinemaManagement.ViewModel.TicketDetailViewModel(ticket, TicketService.GetScreeningOfTicket(ticket));
        }
    }
}
