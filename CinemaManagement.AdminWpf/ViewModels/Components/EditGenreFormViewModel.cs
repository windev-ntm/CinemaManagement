using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.AdminWpf.ViewModels.Components
{
    public partial class EditGenreFormViewModel : ObservableValidator
    {
        private readonly GenreService _genreService;
        private int _id;

        public EditGenreFormViewModel(GenreService genreService)
        {
            _genreService = genreService;
        }

        public event Action? OnCreatedSuccessful;
        public event Action? OnCreatedFailed;

        [ObservableProperty]
        [Required]
        [MinLength(1, ErrorMessage = "Name must not be empty")]
        private string _name = string.Empty;

        [RelayCommand]
        private async Task UpdateGenre()
        {
            ValidateAllProperties();
            if (HasErrors) return;

            bool res = await Task.Run(() => _genreService.UpdateGenreName(_id, Name));

            if (res)
                OnCreatedSuccessful?.Invoke();
            else
                OnCreatedFailed?.Invoke();
        }

        public void SetData(Genre genre)
        {
            _id = genre.Id;
            Name = genre.Name ?? string.Empty;
        }
    }
}
