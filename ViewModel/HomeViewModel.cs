using CinemaManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CinemaManagement.ViewModel
{
    class MovieView : Movie
    {
        public string visibility { get; set; } = "Visible";
        public string NameTrailer { get; set; }
        RelayCommand VisibilityChangedCommand { get; set; } = new RelayCommand(VisibilityChanged);

        RelayCommand ImageMouseEnterCommand { get; set; } = new RelayCommand(ImageMouseEnter);

        private static void ImageMouseEnter()
        {
            throw new NotImplementedException();
        }

        private static void VisibilityChanged()
        {
            
        }
    }

    
    class HomeViewModel : INotifyPropertyChanged
    {
        
        public string findImage { get; set; }
        public string logoPath { get; set; }
        public string filmImage { get; set; }

        private ObservableCollection<MovieView> _movieCollection;

        public MovieView selectedMovie { get; set; }

        public ObservableCollection<MovieView> MovieCollection
        {
            get { return _movieCollection; }
            set { 
                _movieCollection = value; 
                
            }
        }

        public string trailerVideo { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HomeViewModel()
        {
            findImage = Directory.GetCurrentDirectory() + "\\find.png";
            logoPath = Directory.GetCurrentDirectory() + "\\logo.png";
            filmImage = Directory.GetCurrentDirectory() + "\\film1.jpg";
            trailerVideo = Directory.GetCurrentDirectory() + "\\trailer1.mp4";

            MovieCollection = new ObservableCollection<MovieView>();
            MovieCollection.Add(new MovieView
            {
                Name = "The Shawshank Redemption",
                PublishedYear = 1994,
                ImdbRating = 9.3f,
                PosterImg = Directory.GetCurrentDirectory() + "\\film1.jpg",
                Duration = new TimeSpan(2, 22, 0),
                Trailer = Directory.GetCurrentDirectory() + "\\trailer1.mp4",
                NameTrailer="Trailer1"
            });
            MovieCollection.Add(new MovieView
            {
                Name = "The Godfather",
                PublishedYear = 1991,
                ImdbRating = 9.3f,
                PosterImg = Directory.GetCurrentDirectory() + "\\film2.jpg",
                Duration = new TimeSpan(2, 05, 0),
                Trailer = Directory.GetCurrentDirectory() + "\\trailer1.mp4",
                NameTrailer = "Trailer2"
            });
            MovieCollection.Add(new MovieView
            {
                Name = "The Dark Knight",
                PublishedYear = 1992,
                ImdbRating = 9.3f,
                PosterImg = Directory.GetCurrentDirectory() + "\\film3.jpg",
                Duration = new TimeSpan(1, 56, 0),
                Trailer = Directory.GetCurrentDirectory() + "\\trailer1.mp4",
                NameTrailer = "Trailer3"
            });
            MovieCollection.Add(new MovieView
            {
                Name = "The Lord of the Rings: The Return of the King",
                PublishedYear = 1992,
                ImdbRating = 9.3f,
                PosterImg = Directory.GetCurrentDirectory() + "\\film4.jpg",
                Duration = new TimeSpan(1, 12, 5),
                Trailer = Directory.GetCurrentDirectory() + "\\trailer1.mp4",
                NameTrailer = "Trailer4"
            });
        }
    }
}
