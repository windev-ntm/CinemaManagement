// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using CinemaManagement.AdminWpf.Helpers;
using CinemaManagement.AdminWpf.Services;
using CinemaManagement.AdminWpf.ViewModels.Pages;
using CinemaManagement.AdminWpf.ViewModels.Windows;
using CinemaManagement.AdminWpf.Views.Pages;
using CinemaManagement.AdminWpf.Views.Windows;
using CinemaManagement.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Wpf.Ui;

namespace CinemaManagement.AdminWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(AppContext.BaseDirectory); })
            //.ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!); })
            .ConfigureServices((context, services) =>
            {
                // WPF related services
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<IThemeService, ThemeService>();
                services.AddSingleton<ITaskBarService, TaskBarService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IViewProvider, ViewProvider>();

                // Data services
                services.AddSingleton<GenreService>();
                services.AddSingleton<MovieService>();
                services.AddSingleton<MoviePersonService>();
                services.AddSingleton<AdminService>();

                // Main window with navigation
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddSingleton<SignInViewModel>();
                services.AddSingleton<SignInPage>();

                // Top-level pages
                services.AddSingleton<DashboardViewModel>();
                services.AddSingleton<DashboardPage>();
                services.AddSingleton<GenresViewModel>();
                services.AddSingleton<GenresPage>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<SettingsPage>();
                services.AddSingleton<MoviesViewModel>();
                services.AddSingleton<MoviesPage>();
                services.AddSingleton<PeopleViewModel>();
                services.AddSingleton<PeoplePage>();

                // Other pages
                services.AddTransientFromNamespace(
                    "CinemaManagement.AdminWpf.ViewModels",
                    Assembly.GetExecutingAssembly()
                );
                services.AddTransientFromNamespace(
                    "CinemaManagement.AdminWpf.Views",
                    Assembly.GetExecutingAssembly()
                );
            }).Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetRequiredService<T>()
            where T : class
        {
            return _host.Services.GetRequiredService<T>();
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            _host.Start();

            //await Task.Run(() => StartupService.CompileModel());
            StartupService.CompileModel();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var navigationService = _host.Services.GetRequiredService<INavigationService>();
            navigationService.Navigate(typeof(SignInPage));

            var SignInViewModel = _host.Services.GetRequiredService<SignInViewModel>();
            SignInViewModel.OnSignedIn += () =>
            {
                mainWindow.SetControlForMain();
                //navigationService.Navigate(typeof(DashboardPage));
                mainWindow.Navigate(typeof(DashboardPage));
            };
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }
    }
}
