using CinemaManagement.Models;
using CinemaManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Input;
using WpfCarousel.WPFCarouselControl;

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {

        private List<string> cinemaImages;
        private int currentIndex = 0;
        private HomeViewModel homeViewModel;
        private MediaElement _selectedVideoElement;
        private StackPanel tmpStackpanel;


        public Home()
        {
            InitializeComponent();
            homeViewModel = new HomeViewModel();
            DataContext = homeViewModel;
        }

        public static string GetAbsoluteImagePath(Image image)
        {
            if (image == null || image.Source == null)
                return null;

            BitmapImage bitmapImage = image.Source as BitmapImage;
            if (bitmapImage == null)
                return null;

            Uri absoluteUri = new Uri(bitmapImage.UriSource.AbsoluteUri);
            return absoluteUri.AbsoluteUri;
        }

        public static string RemoveFirst8Characters(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 8)
                return input; // Return the original string if it's null, empty, or has less than 8 characters

            return input.Substring(8); // Return the string starting from the 9th character
        }

        public static bool CompareDirectoryPaths(string path1, string path2)
        {
            // Get the full paths of the input directories
            string fullPath1 = System.IO.Path.GetFullPath(path1);
            string fullPath2 = System.IO.Path.GetFullPath(path2);

            // Compare the full paths (case-insensitive comparison)
            return string.Equals(fullPath1, fullPath2, StringComparison.OrdinalIgnoreCase);
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            Grid grid = image.Parent as Grid;
            MovieView selectedItem;

            Debug.WriteLine(grid.Name);
            if(grid.Name == "caro0")
            {
                selectedItem = _carousel.SelectedItem as MovieView;
            }
            else
            {
                selectedItem = _carousel1.SelectedItem as MovieView;
            }
             

            string pathImage = RemoveFirst8Characters(image.Source.ToString()); 
            //Debug.WriteLine(selectedItem.PosterImg);
            Debug.WriteLine("XYZ");

            Debug.WriteLine(homeViewModel.selectedMovie);
            if (image != null && selectedItem != null && CompareDirectoryPaths(pathImage, selectedItem.PosterImg))
            {
                var scaleTransform = new ScaleTransform(1.2, 1.2, 0.5, 0.5);
                image.RenderTransform = scaleTransform;
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                //image.Visibility = Visibility.Collapsed;
                //grid.Children.Remove(image);

                MediaElement videoElement = new MediaElement();
                videoElement.Source = new Uri(Directory.GetCurrentDirectory() + "\\trailer1.mp4");
                videoElement.VerticalAlignment = VerticalAlignment.Center;
                videoElement.HorizontalAlignment = HorizontalAlignment.Center;
                videoElement.Width = image.RenderSize.Width;
                videoElement.Height = image.RenderSize.Height;
                videoElement.LoadedBehavior = MediaState.Manual;
                videoElement.UnloadedBehavior = MediaState.Manual;
                videoElement.MouseLeave += Video_MouseLeave;
                Grid.SetRow(videoElement, 0);

                grid.Children.Remove(tmpStackpanel);

                tmpStackpanel = new StackPanel();
                tmpStackpanel.Orientation = Orientation.Vertical;
                tmpStackpanel.VerticalAlignment = VerticalAlignment.Bottom;
                tmpStackpanel.Background = Brushes.Black;
                tmpStackpanel.Opacity = 0.7;

                Label label = new Label();
                label.Content = "Duration: " + selectedItem.Duration;
                label.FontSize = 20;
                label.Foreground = Brushes.White;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.FontFamily = new FontFamily("Arial");

                Label label1 = new Label();
                label1.Content = "Rating: " + selectedItem.ImdbRating + "/10";
                label1.FontSize = 20;
                label1.Foreground = Brushes.White;
                label1.HorizontalAlignment = HorizontalAlignment.Center;
                label1.FontFamily = new FontFamily("Arial");

                Label label2 = new Label();
                label2.Content = "Published year: " + selectedItem.PublishedYear ;
                label2.FontSize = 20;
                label2.Foreground = Brushes.White;
                label2.HorizontalAlignment = HorizontalAlignment.Center;
                label2.FontFamily = new FontFamily("Arial");

                tmpStackpanel.Children.Add(label);
                tmpStackpanel.Children.Add(label1);

                grid.Children.Add(videoElement);
                grid.Children.Add(tmpStackpanel);
                videoElement.RenderTransform = scaleTransform;
                videoElement.RenderTransformOrigin = new Point(0.5, 0.5);
                videoElement.Play();
                /*                videoElement.Visibility = Visibility.Visible;
                                videoElement.Play();*/
            }
        }

        private void Video_MouseEnter(object sender, MouseEventArgs e) { 

            var videoElement = sender as MediaElement;
            if (videoElement != null)
            {
                videoElement.Visibility = Visibility.Visible;
                Debug.WriteLine("ABC");
                videoElement.Play();
                
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                image.RenderTransform = null;
            }
        }

        private void Video_MouseLeave(object sender, MouseEventArgs e)
        {
            var videoElement = sender as MediaElement;
            Grid grid = videoElement.Parent as Grid;

            if (videoElement != null)
            {
                videoElement.Stop();
                grid.Children.Remove(tmpStackpanel);
                grid.Children.Remove(videoElement);
                
            }
        }


        private void usercontrol_loaded(object sender, RoutedEventArgs e)
        {
            _carousel1.SelectedItem = homeViewModel.MovieCollection[0];
            _carousel.SelectedItem = homeViewModel.MovieCollection[0];
        }

        private void _buttonLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            _carousel.RotateRight();
        }

        private void _buttonRightArrow_Click(object sender, RoutedEventArgs e)
        {
            _carousel.RotateLeft();
        }

        private void _buttonLeftArrow_Click1(object sender, RoutedEventArgs e)
        {
            _carousel1.RotateRight();
        }

        private void _buttonRightArrow_Click1(object sender, RoutedEventArgs e)
        {
            _carousel1.RotateLeft();
        }


    }
}
