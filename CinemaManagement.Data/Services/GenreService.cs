using CinemaManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Data.Services
{
    public class GenreService
    {
        public List<Genre> GetAllGenres()
        {
            using var context = new CinemaManagementContext();
            return context.Genres.AsNoTracking()
                .OrderBy(g => g.Id)
                .ToList();
        }

        public bool UpdateGenreName(int id, string? newName)
        {
            using var context = new CinemaManagementContext();
            var rowsUpdated = context.Genres
                .Where(g => g.Id == id)
                .ExecuteUpdate(setters => setters
                    .SetProperty(g => g.Name, newName));

            return rowsUpdated > 0;
        }

        public bool CreateGenre(string name)
        {
            using var context = new CinemaManagementContext();
            var genre = new Genre { Name = name };
            context.Genres.Add(genre);
            context.SaveChanges();

            return genre.Id > 0;
        }

        public bool DeleteGenre(int id)
        {
            using var context = new CinemaManagementContext();
            try
            {
                var rowsUpdated = context.Genres
                .Where(g => g.Id == id)
                .ExecuteDelete();

                return rowsUpdated > 0;
            }
            catch (Exception)
            {
                throw new Exception("In-use genre can't be deleted");
            }
        }
    }
}
