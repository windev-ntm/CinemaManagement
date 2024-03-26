using CinemaManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
namespace CinemaManagement.Services
{
    public class ScreeningService
    {
        static CinemaManagementContext cinemaManagementContext = new CinemaManagementContext();

        public async Task<List<Screening>> GetScreenings()
        {
            using var context = new CinemaManagementContext();

            return await Task.Run(() => context.Screenings.ToList());
        }
        public async Task<Screening> GetScreeningById(int id)
        {
            var result= await Task.Run(() =>
                cinemaManagementContext.Screenings
                    .Include(screening => screening.Tickets)
                    .Include(screening => screening.Screen)
                    .FirstOrDefault(screening => screening.Id == id));
            return result;
        }
    }
}
