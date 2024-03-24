using CinemaManagement.Data.Models;

namespace CinemaManagement.Data.Services
{
    public class GenreService
    {
        public List<Genre> GetAllGenres()
        {
            using var context = new CinemaManagementContext();
            return context.Genres.ToList();
        }
    }
}
