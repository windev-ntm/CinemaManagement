using CinemaManagement.Data.Models;

namespace CinemaManagement.Data.Services
{
    public class TicketService
    {
        public int GetTotalTicketsSold()
        {
            using var context = new CinemaManagementContext();
            return context.Tickets.Count();
        }

        public int GetTotalValueOfTickets()
        {
            using var context = new CinemaManagementContext();
            return context.Tickets.Sum(t => t.Price);
        }
    }
}
