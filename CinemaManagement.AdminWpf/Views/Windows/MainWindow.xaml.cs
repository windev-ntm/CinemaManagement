// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using CinemaManagement.AdminWpf.ViewModels.Windows;
using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CinemaManagement.AdminWpf.Views.Windows
{
    public partial class MainWindow : INavigationWindow
    {
        private readonly ISnackbarService _snackbarService;
        private readonly INavigationService _navigationService;

        public MainWindow(
            MainViewModel viewModel,
            IPageService pageService,
            INavigationService navigationService,
            IContentDialogService contentDialogService,
            ISnackbarService snackbarService
        )
        {
            _snackbarService = snackbarService;
            _navigationService = navigationService;

            SystemThemeWatcher.Watch(this);

            DataContext = viewModel;

            InitializeComponent();
            SetControlForSignIn();
            contentDialogService.SetContentPresenter(RootContentDialog);
            SetPageService(pageService);
        }

        public void SetControlForSignIn()
        {
            RootNavigation.Visibility = Visibility.Hidden;
            SignInNavigation.Visibility = Visibility.Visible;
            _snackbarService.SetSnackbarPresenter(SignInSnackbarPresenter);
            _navigationService.SetNavigationControl(SignInNavigation);
        }

        public void SetControlForMain()
        {
            SignInNavigation.Visibility = Visibility.Collapsed;
            RootNavigation.Visibility = Visibility.Visible;
            _snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            _navigationService.SetNavigationControl(RootNavigation);
        }

        #region INavigationWindow methods

        public INavigationView GetNavigation() => RootNavigation;

        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService) => RootNavigation.SetPageService(pageService);

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        #endregion INavigationWindow methods

        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Make sure that closing this window will begin the process of closing the application.
            Application.Current.Shutdown();
        }

        INavigationView INavigationWindow.GetNavigation()
        {
            throw new NotImplementedException();
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
