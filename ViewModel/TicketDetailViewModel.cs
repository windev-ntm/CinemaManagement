using CinemaManagement.Models;
using CinemaManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel
{
    public class TicketDetailViewModel
    {
        public ObservableCollection<TicketInfo> SeatList { get; set; }

        public ObservableCollection<TicketInfo> SelectedSeats { get; set; }

        public ScreeningInfo Screening { get; set; }

        public string StartTime { get; set; }   
        public string EndTime { get; set; }
        public string ScreeningName { get; set; }
        public string MovieName { get; set; }
        public string Date { get; set; }
        public TicketDetailViewModel(Ticket ticket, Screening screeningOfTicket)
        {
            screeningOfTicket.Screen = new Screen();
            screeningOfTicket.Screen = TicketService.GetScreenOfScreening(screeningOfTicket);
            TimeSpan durationMovie = TicketService.GetMovieDurationByScreening(screeningOfTicket);
            Screening = new ScreeningInfo(screeningOfTicket, durationMovie);
            StartTime = Screening.StartTime;
            EndTime = Screening.EndTime;
            ScreeningName = Screening.ScreenName;
            Date = Screening.Date;
            MovieName = TicketService.GetMovieNameByScreening(screeningOfTicket);

            SeatList = new ObservableCollection<TicketInfo>();

            int totalSeats = 100;

            if (screeningOfTicket.Screen.Seats != null)
            {
                totalSeats = (int)screeningOfTicket.Screen.Seats;
            }

            bool[] map = new bool[totalSeats + 1];

            map[ticket.Seat] = true;

            for (int i = 0; i < totalSeats; i++)
            {
                if(i == ticket.Seat)
                {
                    continue;
                }
                if (map[i + 1] == true)
                {
                    SeatList.Add(new TicketInfo(i + 1, true));
                }
                else
                {
                    SeatList.Add(new TicketInfo(i + 1, false));
                }
            }

/*            SelectedSeats = new ObservableCollection<TicketInfo>();
            foreach (var seat in seats)
            {
                TicketInfo ticket = new TicketInfo(seat + 1, true);
                ticket.IsSelected = true;
                ticket.IsEnabled = true;
                SelectedSeats.Add(ticket);
            }*/
            

        }
    }
}
