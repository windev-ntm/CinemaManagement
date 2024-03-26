using CinemaManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                .Include(m => m.Certification)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalMovies(int pageSize, string searchText)
        {
            using var context = new CinemaManagementContext();
            var query = context.Movies.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(m => EF.Functions.ILike(m.Name, $"%{searchText}%"));
            }

            return (int)Math.Ceiling((double)query.Count() / pageSize);
        }

        public List<Genre> GetGenresOfMovie(int movieId)
        {
            using var context = new CinemaManagementContext();
            return context.Movies.AsNoTracking()
                .Where(m => m.Id == movieId)
                .SelectMany(m => m.Genres)
                .ToList();
        }

        public bool AddMovie(Movie movie, List<Genre> genres)
        {
            try
            {
                using var context = new CinemaManagementContext();

                ICollection<Genre> collection = new List<Genre>();

                foreach (var genre in genres)
                {
                    collection.Add(context.Genres.FirstOrDefault(g => g.Id == genre.Id));
                }

                movie.Genres = collection;
                context.Movies.Add(movie);

                return context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

        }

        public List<MovieCertification> GetAllCertifications()
        {
            using var context = new CinemaManagementContext();
            return context.MovieCertifications.AsNoTracking().ToList();
        }
    }
}
