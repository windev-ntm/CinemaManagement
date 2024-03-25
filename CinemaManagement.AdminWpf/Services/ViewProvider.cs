using CinemaManagement.AdminWpf.ViewModels.Components;
using CinemaManagement.AdminWpf.ViewModels.Pages;
using CinemaManagement.AdminWpf.Views.Components;
using CinemaManagement.AdminWpf.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CinemaManagement.AdminWpf.Services
{
    public interface IViewProvider
    {
        public Type GetViewType<T>() where T : class;
        public FrameworkElement GetView<T>() where T : class;

        public FrameworkElement GetView(Type viewModelType);

        public FrameworkElement GetView<T>(out T viewModel) where T : class;

        public T GetViewModel<T>() where T : class;

        public object GetViewModel(Type viewModelType);
    }

    public class ViewProvider : IViewProvider
    {
        private readonly Dictionary<Type, Type> _viewMap = new()
        {
            [typeof(DashboardViewModel)] = typeof(DashboardPage),
            [typeof(GenresViewModel)] = typeof(GenresPage),
            [typeof(SettingsViewModel)] = typeof(SettingsPage),
            [typeof(MoviesViewModel)] = typeof(MoviesPage),

            [typeof(EditGenreFormViewModel)] = typeof(EditGenreForm),
            [typeof(AddGenreFormViewModel)] = typeof(AddGenreForm),
            [typeof(AddMovieFormViewModel)] = typeof(AddMovieForm),
        };

        private readonly IServiceProvider _serviceProvider;

        public ViewProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Type GetViewType<T>() where T : class
        {
            if (!_viewMap.TryGetValue(typeof(T), out var viewType))
                throw new InvalidOperationException("Input view model doesn't exist");

            return viewType;
        }


        public FrameworkElement GetView<T>() where T : class
        {
            if (!_viewMap.TryGetValue(typeof(T), out var viewType))
                throw new InvalidOperationException("Input view model doesn't exist");

            if (_serviceProvider.GetService(viewType) is not FrameworkElement view)
                throw new InvalidOperationException("The view doesn't exist");

            return view;
        }

        public FrameworkElement GetView(Type viewModelType)
        {
            if (!_viewMap.TryGetValue(viewModelType, out var viewType))
                throw new InvalidOperationException("The view doesn't exist");

            if (_serviceProvider.GetService(viewType) is not FrameworkElement view)
                throw new InvalidOperationException("The view doesn't exist");

            return view;
        }

        public T GetViewModel<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public object GetViewModel(Type viewModelType)
        {
            return _serviceProvider.GetRequiredService(viewModelType);
        }

        public FrameworkElement GetView<T>(out T viewModel) where T : class
        {
            var view = GetView<T>();
            viewModel = (T)view.DataContext;

            if (viewModel is null)
                throw new InvalidOperationException("The view doesn't have view model");

            return view;
        }
    }
}
