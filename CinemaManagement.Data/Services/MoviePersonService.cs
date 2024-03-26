using CinemaManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManagement.Data.Services
{
    public class MoviePersonService
    {
        public List<PersonInMovie> GetAllPeopleInMovie(int movieId)
        {
            using var context = new CinemaManagementContext();
            return context.PersonInMovies.AsNoTracking()
                .Where(pim => pim.MovieId == movieId)
                .Include(pim => pim.Person)
                .ToList();
        }

        public List<Person> GetAllPeople(int page, int pageSize, string searchText)
        {
            using var context = new CinemaManagementContext();

            var query = context.People.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(p => EF.Functions.ILike(p.Name, $"%{searchText}%"));
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalPages(int pageSize, string searchText)
        {
            using var context = new CinemaManagementContext();
            var query = context.People.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(p => EF.Functions.ILike(p.Name, $"%{searchText}%"));
            }

            return (int)Math.Ceiling((double)query.Count() / pageSize);
        }

        public bool AddPerson(string name, string avatar, string bio)
        {
            using var context = new CinemaManagementContext();

            var person = new Person
            {
                Name = name,
                Avatar = avatar,
                Bio = bio
            };

            context.People.Add(person);
            return context.SaveChanges() > 0;
        }

        public bool UpdatePerson(Person person)
        {
            using var context = new CinemaManagementContext();

            var rowsUpdated = context.People
                .Where(p => p.Id == person.Id)
                .ExecuteUpdate(setters => setters
                    .SetProperty(p => p.Name, person.Name)
                    .SetProperty(p => p.Avatar, person.Avatar)
                    .SetProperty(p => p.Bio, person.Bio));

            return rowsUpdated > 0;
        }

        public bool AddPersonToMovie(int personId, int movieId, string role)
        {
            using var context = new CinemaManagementContext();

            var personInMovie = new PersonInMovie
            {
                PersonId = personId,
                MovieId = movieId,
                Role = role
            };

            context.PersonInMovies.Add(personInMovie);
            return context.SaveChanges() > 0;
        }
    }
}
