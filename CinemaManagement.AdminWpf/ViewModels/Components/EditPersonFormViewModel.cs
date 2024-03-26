using CinemaManagement.AdminWpf.ViewModels.Pages;
using CinemaManagement.Data.Models;
using CinemaManagement.Data.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace CinemaManagement.AdminWpf.ViewModels.Components
{
    public partial class EditPersonFormViewModel : ObservableValidator
    {
        private readonly MoviePersonService _moviePersonService;
        private readonly IServiceProvider _serviceProvider;
        private int _id;

        public EditPersonFormViewModel(
            MoviePersonService moviePersonService,
            IServiceProvider serviceProvider
        )
        {
            _moviePersonService = moviePersonService;
            _serviceProvider = serviceProvider;

            var parentViewModel = _serviceProvider.GetRequiredService<PeopleViewModel>();
            parentViewModel.Configure(this);
        }

        public event Action? OnCreatedSuccessful;
        public event Action? OnCreatedFailed;

        [ObservableProperty]
        [Required]
        [MinLength(1, ErrorMessage = "Name must not be empty")]
        private string _name = string.Empty;

        [ObservableProperty]
        [CustomValidation(typeof(EditPersonFormViewModel), nameof(ValidateUrl))]
        private string _avatar = string.Empty;

        [ObservableProperty]
        private string _bio = string.Empty;

        private bool CanEdit
        {
            get
            {
                ValidateAllProperties();
                return !HasErrors;
            }
        }

        [RelayCommand(CanExecute = nameof(CanEdit))]
        private async Task Edit()
        {
            bool res = await Task.Run(() => _moviePersonService.UpdatePerson(
                new Person()
                {
                    Id = _id,
                    Name = Name,
                    Avatar = Avatar,
                    Bio = Bio
                }
            ));

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

        public void Initialize(Person person)
        {
            _id = person.Id;
            Name = person.Name ?? string.Empty;
            Avatar = person.Avatar ?? string.Empty;
            Bio = person.Bio ?? string.Empty;
        }
    }
}
