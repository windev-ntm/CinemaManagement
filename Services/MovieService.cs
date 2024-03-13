using CinemaManagement.Models;

namespace CinemaManagement
{
    public class MovieService
    {
        static CinemaManagementContext cinemaManagementContext = new CinemaManagementContext();

        public List<Movie> GetMovies()
        {
            var query = from movie in cinemaManagementContext.Movies
                        join certification in cinemaManagementContext.MovieCertifications
                        on movie.CertificationId equals certification.Id
                        select new Movie
                        {
                            Id = movie.Id,
                            Name = movie.Name,
                            PublishedYear = movie.PublishedYear,
                            Duration = movie.Duration,
                            ImdbRating = movie.ImdbRating,
                            CertificationId = movie.CertificationId,
                            PosterImg = movie.PosterImg,
                            Trailer = movie.Trailer,
                            // Thêm các trường khác của Movie cần lấy

                            // Thêm trường CertificationName từ bảng MovieCertification
                            CertificationName = certification.Name
                        };

            return [.. query];
        }

        public List<Movie> GetMovieBySearch(string name, string sortBy, int sortDirection)
        {
            // Lấy danh sách phim từ nguồn dữ liệu (ví dụ: database, service, ...)
            List<Movie> movies = GetMovies();

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

    }
}
