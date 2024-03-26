using CinemaManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Data.Services
{
    public class ScreeningService
    {
        public List<Screening> GetAllScreeningsOfMovie(int movieId)
        {
            using var context = new CinemaManagementContext();
            return context.Screenings.AsNoTracking()
                .Where(s => s.MovieId == movieId).ToList();
        }
    }
}
