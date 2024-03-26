using CinemaManagement.AdminWpf.ViewModels.Pages;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.AdminWpf.ViewModels.Components
{
    public partial class AddPersonFormViewModel : ObservableValidator
    {
        private readonly MoviePersonService _moviePersonService;
        private readonly IServiceProvider _serviceProvider;

        public AddPersonFormViewModel(
            MoviePersonService moviePersonService,
            IServiceProvider serviceProvider
        )
        {
            _moviePersonService = moviePersonService;
            _serviceProvider = serviceProvider;

            var parentViewModel = _serviceProvider.GetRequiredService<PeopleViewModel>();
            parentViewModel.RegisterEvents(this);
        }

        public event Action? OnCreatedSuccessful;
        public event Action? OnCreatedFailed;

        [ObservableProperty]
        [Required]
        [MinLength(1, ErrorMessage = "Name must not be empty")]
        private string _name = string.Empty;

        [ObservableProperty]
        [CustomValidation(typeof(AddPersonFormViewModel), nameof(ValidateUrl))]
        private string _avatar = string.Empty;

        [ObservableProperty]
        private string _bio = string.Empty;

        private bool CanCreate
        {
            get
            {
                ValidateAllProperties();
                return !HasErrors;
            }
        }

        [RelayCommand(CanExecute = nameof(CanCreate))]
        private async Task Create()
        {
            bool res = await Task.Run(() => _moviePersonService.AddPerson(Name, Avatar, Bio));

            if (res)
                OnCreatedSuccessful?.Invoke();
            else
                OnCreatedFailed?.Invoke();
        }

        public static ValidationResult? ValidateUrl(string url, ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(url)) return ValidationResult.Success;

            bool result = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result) return ValidationResult.Success;

            return new("Invalid URL format.");
        }
    }
}
