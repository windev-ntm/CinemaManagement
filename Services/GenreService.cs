using CinemaManagement.Models;

namespace CinemaManagement.Services
{
    class GenreService
    {
        static CinemaManagementContext cinemaManagementContext = new CinemaManagementContext();

        public async Task<List<Genre>> GetGenres()
        {
            return await Task.Run(() => cinemaManagementContext.Genres.ToList());
        }
    }
}
