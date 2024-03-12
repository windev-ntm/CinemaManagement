using CinemaManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Data.Services
{
    public class MoviesService
    {
        public async Task<List<Movie>> GetAllMovies()
        {
            using var context = new CinemaManagementContext();
            return await context.Movies.ToListAsync();
        }

        public List<Movie> GetAllMoviesSync()
        {
            using (var context = new CinemaManagementContext())
            {
                return context.Movies.ToList();
            }
        }
    }
}
