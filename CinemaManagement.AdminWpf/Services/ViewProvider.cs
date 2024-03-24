using CinemaManagement.AdminWpf.ViewModels.Components;
using CinemaManagement.AdminWpf.ViewModels.Pages;
using CinemaManagement.AdminWpf.ViewModels.Windows;
using CinemaManagement.AdminWpf.Views.Components;
using CinemaManagement.AdminWpf.Views.Pages;
using CinemaManagement.AdminWpf.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CinemaManagement.AdminWpf.Services
{
    public interface IViewProvider
    {
        public FrameworkElement? GetView<T>() where T : class;

        public FrameworkElement? GetView(Type viewModelType);

        public FrameworkElement? GetViewAndUpdateContext(object viewModel);

        public T? GetViewModel<T>() where T : class;

        public object? GetViewModel(Type viewModelType);
    }

    public class ViewProvider : IViewProvider
    {
        private readonly Dictionary<Type, Type> _viewMap = new()
        {
            [typeof(MainViewModel)] = typeof(MainWindow),
            [typeof(DashboardViewModel)] = typeof(DashboardPage),
            [typeof(GenresViewModel)] = typeof(GenresPage),
            [typeof(SettingsViewModel)] = typeof(SettingsPage),

            [typeof(AddGenreFormViewModel)] = typeof(GenreForm),
        };

        private readonly IServiceProvider _serviceProvider;

        public ViewProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public FrameworkElement? GetView<T>() where T : class
        {
            if (!_viewMap.TryGetValue(typeof(T), out var viewType))
                return null;

            if (!typeof(FrameworkElement).IsAssignableFrom(viewType))
                throw new InvalidOperationException("The page should be a WPF element.");

            return _serviceProvider.GetService(viewType) as FrameworkElement;
        }

        public FrameworkElement? GetView(Type viewModelType)
        {
            if (!_viewMap.TryGetValue(viewModelType, out var viewType))
                return null;

            if (!typeof(FrameworkElement).IsAssignableFrom(viewType))
                throw new InvalidOperationException("The page should be a WPF element.");

            return _serviceProvider.GetService(viewType) as FrameworkElement;
        }

        public T? GetViewModel<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }

        public object? GetViewModel(Type viewModelType)
        {
            return _serviceProvider.GetService(viewModelType);
        }

        public FrameworkElement? GetViewAndUpdateContext(object viewModel)
        {
            var view = GetView(viewModel.GetType());
            if (view is null)
                return null;

            view.DataContext = viewModel;

            return view;
        }
    }
}
