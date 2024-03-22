using CinemaManagement.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Input;
using CinemaManagement.View;

namespace CinemaManagement.ViewModel
{
    class MovieDetailViewModel : INotifyPropertyChanged
    {

        private Movie movie;
        private MovieService movieService;

        public MovieDetailViewModel(Movie movie)
        {
            this.movie = movie;
            movieService = new MovieService();
            _actorList = movieService.GetActors(movie);
            _directorList = movieService.GetDirectors(movie);
        }


        private ICommand _playButton;
        private ICommand _buyButton;
        private ICommand _screeningSelected;

        private List<string> _actorList;
        private List<string> _directorList;

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public List<string> DirectorList
        {
            get => _directorList;
        }

        public List<string> ActorList
        {
            get => _actorList;
        }




        public List<Genre> GenreList
        {
            get => (List<Genre>)movie.Genres;
        }


        public ICommand ItemSelectedCommand
        {
            get
            {
                if (_screeningSelected == null)
                {
                    _screeningSelected = new RelayCommand<string>(param => HandleScreeningSelected(param));
                }
                return _screeningSelected;
            }
        }

        private void HandleScreeningSelected(string SelectedItem1)
        {
            if (SelectedItem1 != null)
            {
                BuyTicketView buyTicketView = new BuyTicketView();
                buyTicketView.Show();
            }
        }

        public ICommand BuyButton
        {
            get
            {
                if (_buyButton == null)
                {
                    _buyButton = new RelayCommand(param => HandleBuyButton());
                }
                return _buyButton;
            }
        }

        public ICommand PlayButton
        {
            get
            {
                if (_playButton == null)
                {
                    _playButton = new RelayCommand(param => HandlePlayButton());
                }
                return _playButton;
            }
        }

        private void HandleBuyButton()
        {
            MessageBox.Show("Buy button clicked");
        }

        private void HandlePlayButton()
        {
            //Do nothing
        }



        public string Name
        {
            get => movie.Name;
            set
            {
                movie.Name = value;
                OnPropertyChanged(nameof(Name));
            }

        }
        public int PublishedYear
        {
            get => (int)movie.PublishedYear;
            set
            {
                movie.PublishedYear = value;
                OnPropertyChanged(nameof(PublishedYear));
            }
        }
        public TimeSpan Duration
        {
            get => (TimeSpan)movie.Duration;
            set
            {
                movie.Duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
        public float ImdbRating
        {
            get => (float)movie.ImdbRating;
            set
            {
                movie.ImdbRating = value;
                OnPropertyChanged(nameof(ImdbRating));
            }
        }

        public MovieCertification Certification
        {
            get => movie.Certification;
            set
            {
                movie.Certification = value;
                OnPropertyChanged(nameof(Certification));
            }
        }

        public string PosterImg
        {
            get => movie.PosterImg;
            set
            {
                movie.PosterImg = value;
                OnPropertyChanged(nameof(PosterImg));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
