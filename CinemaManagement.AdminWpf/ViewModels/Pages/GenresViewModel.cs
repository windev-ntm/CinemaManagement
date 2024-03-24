using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.ViewModels.Pages
{
    public partial class GenresViewModel : ObservableObject, INavigationAware
    {
        private readonly GenreService _genreService;

        private bool _isInitialized = false;


        private ObservableCollection<Genre> _genres = [];
        public ObservableCollection<Genre> Genres
        {
            get => _genres;
        }

        public GenresViewModel(GenreService genreService)
        {
            _genreService = genreService;
        }

        public void OnNavigatedFrom()
        {
        }

        public async void OnNavigatedTo()
        {
            if (_isInitialized) return;

            await InitializeViewModel();
        }

        public async Task InitializeViewModel()
        {
            var res = await Task.Run(() => _genreService.GetAllGenres());
            _genres.Clear();
            foreach (var genre in res)
            {
                _genres.Add(genre);
            }

            _isInitialized = true;
        }
    }
}
