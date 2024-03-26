using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CinemaManagement.AdminWpf.ViewModels.Components
{
    public partial class SelectGenresViewModel : ObservableObject
    {
        private readonly GenreService _genreService;
        private readonly IServiceProvider _serviceProvider;

        public SelectGenresViewModel(GenreService genreService, IServiceProvider serviceProvider)
        {
            _genreService = genreService;
            _serviceProvider = serviceProvider;
        }

        public ObservableCollection<Genre> Genres { get; private set; } = [];

        public async Task LoadData()
        {
            var res = Task.Run(() => _genreService.GetAllGenres());
            Genres.Clear();
            foreach (var genre in await res)
            {
                Genres.Add(genre);
            }
        }

    }
}
