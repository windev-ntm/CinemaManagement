using CinemaManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Services
{
    public class TicketService
    {
        static CinemaManagementContext cinemaManagementContext = new CinemaManagementContext();

        //get all tickets
        public List<Ticket> GetTickets()
        {
            return cinemaManagementContext.Tickets.ToList();
        }

        //get ticket of a user by user id (table invoice join ticket) and save screening room, movie, time start
        static public List<Ticket> GetTicketsByUserId(int userId)
        {
            return cinemaManagementContext.Tickets.Where(t => t.Invoice.UserId == userId).ToList();
        }

        //get screening information of a ticket
        static public Screening GetScreeningOfTicket(Ticket ticket)
        {
            return cinemaManagementContext.Screenings.Where(s => s.Id == ticket.ScreeningId).FirstOrDefault();
        }

        // Get Screen by ScreenId of Screening Variable
        static public Screen GetScreenOfScreening(Screening screening)
        {
            return cinemaManagementContext.Screens.Where(s => s.Id == screening.ScreenId).FirstOrDefault();
        }

        //Get movie name by Screening
        static public string GetMovieNameByScreening(Screening screening)
        {
            return cinemaManagementContext.Movies.Where(m => m.Id == screening.MovieId).FirstOrDefault().Name;
        }

        //Get movie duration by Screening
        static public TimeSpan GetMovieDurationByScreening(Screening screening)
        {
            return cinemaManagementContext.Movies.Where(m => m.Id == screening.MovieId).FirstOrDefault().Duration.Value;
        }

        //Get movie name by Ticket
        static public string GetMovieNameByTicket(Ticket ticket)
        {
            return cinemaManagementContext.Movies.Where(m => m.Id == cinemaManagementContext.Screenings.Where(s => s.Id == ticket.ScreeningId).FirstOrDefault().MovieId).FirstOrDefault().Name;
        }


        
    }
}
