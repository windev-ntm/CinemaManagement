using CinemaManagement.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        SearchViewModel searchViewModel;

        public Search()
        {
            InitializeComponent();
            searchViewModel = new CinemaManagement.SearchViewModel();
            DataContext = searchViewModel;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Kiểm tra nếu double-click được thực hiện trên một ListViewItem
            if (sender is ListViewItem)
            {
                // Lấy dữ liệu tương ứng với item được double-click
                Movie selectedMovie = (Movie)((ListViewItem)sender).Content;

                searchViewModel.ShowMovieDetail(selectedMovie);

                // Xử lý logic khi double-click vào một item trong ListView
            }
        }
    }
}
