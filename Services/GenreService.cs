using CinemaManagement.Models;

namespace CinemaManagement.Services
{
    class GenreService
    {
        static CinemaManagementContext cinemaManagementContext = new CinemaManagementContext();

        public async Task<List<Genre>> GetGenres()
        {
            using var context = new CinemaManagementContext();
            return await Task.Run(() => context.Genres.ToList());
        }
    }
}
