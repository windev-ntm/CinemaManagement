using CinemaManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

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
            return context.Invoices.Sum(t => t.TotalPrice);
        }

        // datetime - sum - count
        public List<Tuple<DateTime, int, int>> GetRevenueByDate()
        {
            using var context = new CinemaManagementContext();

            var query = from invoice in context.Invoices
                        group invoice by invoice.BoughtTime.Value.Date into g
                        select new Tuple<DateTime, int, int>(
                            g.Key, g.Sum(i => i.TotalPrice), g.Count());

            return query.ToList();
        }

        public List<Tuple<DateTime, int, int>> GetRevenueByDateForMovie(int movieId)
        {
            using var context = new CinemaManagementContext();

            var query = from ticket in context.Tickets
                        join invoice in context.Invoices on ticket.InvoiceId equals invoice.Id
                        join screening in context.Screenings on ticket.ScreeningId equals screening.Id
                        where screening.MovieId == movieId
                        group ticket by invoice.BoughtTime.Value.Date into g
                        select new Tuple<DateTime, int, int>(
                            g.Key,
                            g.Sum(t => t.Price),
                            g.Count());
            return query.ToList();
        }

        public List<Tuple<DateTime, int, int>> GetRevenueByDateForScreening(int screeningId)
        {
            using var context = new CinemaManagementContext();

            var query = from ticket in context.Tickets
                        join invoice in context.Invoices on ticket.InvoiceId equals invoice.Id
                        join screening in context.Screenings on ticket.ScreeningId equals screening.Id
                        where screening.Id == screeningId
                        group ticket by invoice.BoughtTime.Value.Date into g
                        select new Tuple<DateTime, int, int>(
                            g.Key,
                            g.Sum(t => t.Price),
                            g.Count());
            return query.ToList();
        }
    }
}
