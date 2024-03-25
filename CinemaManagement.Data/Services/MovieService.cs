using CinemaManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Data.Services
{
    public class MovieService
    {
        public List<Movie> GetAllMovies()
        {
            using var context = new CinemaManagementContext();
            return context.Movies.AsNoTracking()
                .Include(m => m.Certification)
                .ToList();
        }

        public List<Movie> GetMovies(int page, int pageSize, string? searchText = default)
        {
            using var context = new CinemaManagementContext();

            var query = context.Movies.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(m => EF.Functions.ILike(m.Name, $"%{searchText}%"));
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalPages(int pageSize, string searchText)
        {
            using var context = new CinemaManagementContext();
            var query = context.Movies.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(m => EF.Functions.ILike(m.Name, $"%{searchText}%"));
            }

            return (int)Math.Ceiling((double)query.Count() / pageSize);
        }
    }
}
