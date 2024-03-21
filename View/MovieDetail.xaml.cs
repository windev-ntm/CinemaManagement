using CinemaManagement.Models;
using System.Windows;
using CinemaManagement.ViewModel;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for MovieDetail.xaml  
    /// </summary>
    public partial class MovieDetail : Window
    {
        public MovieDetail(Movie movie)
        {
            InitializeComponent();
            DataContext = new MovieDetailViewModel(movie);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Thực hiện logic để phát video ở đây
            // Ví dụ: Bạn có thể set Source cho MediaElement và sau đó gọi Play
            mediaPlayer.Source = new Uri("D:/Downloads/demo.mp4");
            mediaPlayer.Play();
            playButton.Visibility = Visibility.Hidden;
            playButtonText.Visibility = Visibility.Hidden;
        }
        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            playButton.Visibility = Visibility.Visible;
            playButtonText.Visibility = Visibility.Visible;
        }
    }
}
/*            < ui:TextBlock Grid.ColumnSpan = "2" Grid.Column = "3" Grid.Row = "4" Grid.RowSpan = "4"
                           HorizontalAlignment = "Center" VerticalAlignment = "Top"
                           Text = "Buy ticket now" FontSize = "25" FontFamily = "Roboto"
                           Foreground = "White" Margin = "0,20,0,0" />*/