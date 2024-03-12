using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using System.Collections.ObjectModel;

namespace CinemaManagement.AdminWpf.ViewModels.Controls
{
    public partial class MovieListViewModel : ObservableObject
    {
        private bool _isLoaded = false;
        private readonly MoviesService _moviesService;

        [ObservableProperty]
        private int _test = 0;

        public ObservableCollection<Movie> _movies;
        public IEnumerable<Movie> Movies => _movies;


        public MovieListViewModel(MoviesService moviesService)
        {
            _moviesService = moviesService;
            _movies = [];
        }

        [RelayCommand]
        private async Task Load()
        {
            if (_isLoaded)
            {
                return;
            }

            var res = await Task.Run(_moviesService.GetAllMovies);
            _movies.Clear();
            foreach (var movie in res)
            {
                _movies.Add(movie);
            }
            _isLoaded = true;
        }
    }
}
