using CinemaManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CinemaManagement
{
    public class MovieService
    {
        static CinemaManagementContext cinemaManagementContext = new CinemaManagementContext();

        public async Task<List<Movie>> GetMovies()
        {
            using var context = new CinemaManagementContext();
            var result = await context.Movies
                        .Include(movie => movie.Genres)
                        .Include(movie => movie.Certification)
                        .ToListAsync();
            return result;
        }

        public async Task<List<Movie>> GetMovieBySearch(string name, string sortBy, int sortDirection)
        {
            // Lấy danh sách phim từ nguồn dữ liệu (ví dụ: database, service, ...)
            List<Movie> movies = await GetMovies();

            // Áp dụng các điều kiện lọc nếu có
            if (!string.IsNullOrEmpty(name))
            {
                movies = movies.Where(movie => movie.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                // Sắp xếp theo tiêu chí
                switch (sortBy.ToLower())
                {
                    case "alphabetical":
                        movies = movies.OrderBy(movie => movie.Name).ToList();
                        break;
                    case "runtime":
                        movies = movies.OrderBy(movie => movie.Duration).ToList();
                        break;
                    case "release date":
                        movies = movies.OrderBy(movie => movie.PublishedYear).ToList();
                        break;
                    case "imdb rating":
                        movies = movies.OrderBy(movie => movie.ImdbRating).ToList();
                        break;
                }
            }

            // Áp dụng hướng sắp xếp
            if (sortDirection == 1)
            {
                movies.Reverse();
            }
            return movies;
        }

        public async Task<List<Movie>> GetMovieByFilter(string name, string sortBy, int sortDirection, int startYear = 0, int endYear = 0, int startDuration = 0, int endDuration = 0, double startRating = 0, double endRating = 0, List<int> genreIds = null)
        {
            // Lấy danh sách phim từ nguồn dữ liệu (ví dụ: database, service, ...)
            List<Movie> movies = await GetMovies();
            {
                try
                {
                    // Áp dụng các điều kiện lọc nếu có
                    if (!string.IsNullOrEmpty(name))
                    {
                        movies = movies.Where(movie => movie.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    if (startYear != 0)
                    {
                        movies = movies.Where(movie => movie.PublishedYear >= startYear).ToList();
                    }

                    if (endYear != 0)
                    {
                        movies = movies.Where(movie => movie.PublishedYear <= endYear).ToList();
                    }

                    if (startDuration != 0)
                    {
                        movies = movies.Where(movie =>
                        {
                            TimeSpan? duration = movie.Duration;
                            int timeSpanMinutes = movie.Duration.HasValue ? (int)movie.Duration.Value.TotalMinutes : 0;

                            return timeSpanMinutes >= startDuration;
                        }).ToList();
                    }

                    if (endDuration != 0)
                    {
                        movies = movies.Where(movie =>
                        {
                            TimeSpan? duration = movie.Duration;
                            int timeSpanMinutes = movie.Duration.HasValue ? (int)movie.Duration.Value.TotalMinutes : 0;

                            return timeSpanMinutes <= endDuration;

                        }).ToList();
                    }

                    if (startRating != 0)
                    {
                        movies = movies.Where(movie => movie.ImdbRating >= startRating).ToList();
                    }

                    if (endRating != 0)
                    {
                        movies = movies.Where(movie => movie.ImdbRating <= endRating).ToList();
                    }

                    if (genreIds != null && genreIds.Count > 0)
                    {
                        movies = movies.Where(movie => movie.Genres.Any(movieGenre => genreIds.Contains(movieGenre.Id))).ToList();
                    }

                    if (!string.IsNullOrEmpty(sortBy))
                    {

                        // Sắp xếp theo tiêu chí
                        switch (sortBy.ToLower())
                        {
                            case "alphabetical":
                                movies = movies.OrderBy(movie => movie.Name).ToList();
                                break;
                            case "runtime":
                                movies = movies.OrderBy(movie => movie.Duration).ToList();
                                break;
                            case "release date":
                                movies = movies.OrderBy(movie => movie.PublishedYear).ToList();
                                break;
                            case "imdb rating":
                                movies = movies.OrderBy(movie => movie.ImdbRating).ToList();
                                break;
                        }
                    }

                    // Áp dụng hướng sắp xếp
                    if (sortDirection == 1)

                    {
                        movies.Reverse();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return movies;
            }
        }

        public async Task<List<Movie>> GetPaginatableMovies(int pageNumber = 1, string name = null, string sortBy = null, int sortDirection = 0, int startYear = 0, int endYear = 0, int startDuration = 0, int endDuration = 0, double startRating = 0, double endRating = 0, List<int> genreIds = null)
        {
            int pageSize = 6;
            List<Movie> movies = await GetMovieByFilter(name, sortBy, sortDirection, startYear, endYear, startDuration, endDuration, startRating, endRating, genreIds);
            var result = movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }
    }
}
