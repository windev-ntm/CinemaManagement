using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaManagement.AdminWpf.ViewModels.Components
{
    public partial class AddGenreFormViewModel : ObservableObject
    {
        private readonly GenreService _genreService;

        [ObservableProperty]
        private string _genreName = string.Empty;

        public AddGenreFormViewModel(GenreService genreService)
        {
            _genreService = genreService;
        }

        public event Action? OnCreatedSuccessful;

        public event Action? OnCreatedFailed;

        [RelayCommand]
        private async Task CreateGenre()
        {
            bool res = await Task.Run(() => _genreService.CreateGenre(GenreName));

            if (res)
                OnCreatedSuccessful?.Invoke();
            else
                OnCreatedFailed?.Invoke();
        }
    }
}
