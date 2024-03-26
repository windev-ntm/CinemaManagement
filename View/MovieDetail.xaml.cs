using CinemaManagement.Models;
using System.Windows;
using CinemaManagement.ViewModel;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media;
using ControlzEx.Standard;
namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for MovieDetail.xaml  
    /// </summary>
    public partial class MovieDetail : Window
    {
        public MovieDetail()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Thực hiện logic để phát video ở đây
            // Ví dụ: Bạn có thể set Source cho MediaElement và sau đó gọi Play

            string curDirectory = Global.getCurDirectoryOfProject();
            mediaPlayer.Source = new Uri(System.IO.Path.Combine(curDirectory, "Resources/trailer1.mp4"));
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

/*
            < ui:Button Command = "{Binding BuyButton}"
                            Grid.ColumnSpan="2" Grid.Column="3" Grid.Row="4" Grid.RowSpan="4"
                           HorizontalAlignment="Center" VerticalAlignment="Top"
                           FontSize="30" Background="#FF2E8B57" 
                           Foreground="White" FontFamily="Roboto" BorderThickness="0"
                           Height="70" Width="300"
                           CornerRadius="17" 
                           >
                <ui:TextBlock Grid.ColumnSpan = "2" Grid.Column = "3" Grid.Row = "4" Grid.RowSpan = "4"
                           HorizontalAlignment = "Center" VerticalAlignment = "Top"
                           Text = "Buy ticket now" FontSize = "25" FontFamily = "Roboto"
                           Foreground = "White" Margin = "0,0,0,0" />

            </ ui:Button >*/