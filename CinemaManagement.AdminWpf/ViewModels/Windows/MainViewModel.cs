// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using CommunityToolkit.Mvvm.ComponentModel;

namespace CinemaManagement.AdminWpf.ViewModels.Windows
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Cinema management - Admin";

        //[ObservableProperty]
        //private ObservableCollection<INavigationViewItem> _menuItems =
        //[
        //    new NavigationViewItem()
        //    {
        //        Content = "Dashboard",
        //        Icon = new SymbolIcon { Symbol = SymbolRegular.Board24 },
        //        TargetPageType = typeof(DashboardViewModel),
        //    },
        //];

        //[ObservableProperty]
        //private ObservableCollection<INavigationViewItem> _footerMenuItems =
        //[
        //    new NavigationViewItem()
        //    {
        //        Content = "Settings",
        //        Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
        //        TargetPageType = typeof(SettingsViewModel),
        //    }
        //];

        //[ObservableProperty]
        //private ObservableCollection<MenuItem> _trayMenuItem =
        //[
        //    new MenuItem { Header = "Home", Tag = "tray_home" }
        //];
    }
}
