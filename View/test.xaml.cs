using CinemaManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    /// 
    public partial class test : Window
    {

        private readonly CinemaManagementContext _context = new CinemaManagementContext();


        public test()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.Movies.Load();

            MovieDataGrid.ItemsSource = _context.Movies.Local.ToObservableCollection();
        }
    }
}
