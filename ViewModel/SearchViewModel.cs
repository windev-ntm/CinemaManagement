using CinemaManagement.Models;
using CinemaManagement.View;
using CinemaManagement.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
namespace CinemaManagement
{
    internal class SearchViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Movie> listFilm;

        private MovieService movieService;

        private ICommand _sizeChangedCommand, _searchCommand, _sortDirectionCommand, _filterCommand;
        private ICommand _backButton, _nextButton;

        private bool _enableBackButton, _enableNextButton;
        private double _actualWindowHeight;

        private string _searchText;
        private string _selectedSortType;
        private List<string> _sortType;
        private FilterSubScreenViewModel vm;
        private int _currentPage;

        public SearchViewModel()
        {
            _currentPage = 1;
            _enableBackButton = true;
            _enableNextButton = true;

            SortDirection = 0;
            _sortType = new List<string> { "IMDB Rating", "Release Date", "Alphabetical", "Runtime" };
            movieService = new MovieService();
            listFilm = new ObservableCollection<Movie>();

            vm = new FilterSubScreenViewModel();
            LoadData();
        }

        private async Task LoadData()
        {
            List<Movie> movies = await movieService.GetPaginatableMovies(1);
            foreach (Movie movie in movies)
            {
                listFilm.Add(movie);
            }
        }


        public ICommand BackButton
        {
            get
            {
                if (_backButton == null)
                {
                    _backButton = new RelayCommand(param => HandleBackButton());
                }
                return _backButton;
            }
        }

        public ICommand NextButton
        {
            get
            {
                if (_nextButton == null)
                {
                    _nextButton = new RelayCommand(param => HandleNextButton());
                }
                return _nextButton;
            }
        }

        public bool EnableBackButton
        {
            get { return _enableBackButton; }
            set
            {
                _enableBackButton = value;
                OnPropertyChanged(nameof(EnableBackButton));
            }
        }
        public bool EnableNextButton
        {
            get { return _enableNextButton; }
            set
            {
                _enableNextButton = value;
                OnPropertyChanged(nameof(EnableNextButton));
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new RelayCommand(param => HandleFilterFilmButton());
                }
                return _filterCommand;
            }
        }

        public int SortDirection;

        public ICommand SortDirectionCommand
        {
            get
            {
                if (_sortDirectionCommand == null)
                {
                    _sortDirectionCommand = new RelayCommand(param => HandleSortDirectionButton());
                }
                return _sortDirectionCommand;
            }
        }

        public string SelectedSortType
        {
            get { return _selectedSortType; }
            set
            {
                _selectedSortType = value;
                OnPropertyChanged(nameof(SelectedSortType));
            }
        }


        public List<string> SortType
        {
            get { return _sortType; }
            set
            {
                _sortType = value;
                OnPropertyChanged(nameof(SortType));
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => HandleSearchButton());
                }
                return _searchCommand;
            }
        }



        public void ShowMovieDetail(Movie movie)
        {
            if (movie == null) return;

            View.MovieDetail movieDetail = new View.MovieDetail(movie);
            movieDetail.Show();
        }

        private async Task HandleNextButton()
        {
            List<Movie> movies = await movieService.GetPaginatableMovies(_currentPage, SearchText, SelectedSortType, SortDirection, vm.StartReleaseYear, vm.EndReleaseYear, vm.StartDuration, vm.EndDuration, vm.StartRating, vm.EndRating, vm.SelectedGenres);
            if (movies.Count == 0)
            {
                MessageBox.Show("You are at the last page");
                return;
            }
            _currentPage++;
            _enableBackButton = true;

            listFilm.Clear();
            foreach (var item in movies)
            {
                listFilm.Add(item);
            }
            EnableBackButton = true;
        }
        private async Task HandleBackButton()
        {
            if (_currentPage == 1)
            {
                MessageBox.Show("You are at the first page");
                return;
            }
            _currentPage--;

            List<Movie> movies = await movieService.GetPaginatableMovies(_currentPage, SearchText, SelectedSortType, SortDirection, vm.StartReleaseYear, vm.EndReleaseYear, vm.StartDuration, vm.EndDuration, vm.StartRating, vm.EndRating, vm.SelectedGenres);
            listFilm.Clear();
            foreach (var item in movies)
            {
                listFilm.Add(item);
            }
        }



        private async Task HandleFilterFilmButton()
        {
            vm = new FilterSubScreenViewModel(vm);
            FilterSubScreen filterSubScreen = new FilterSubScreen(vm);
            filterSubScreen.ShowDialog();

            List<Movie> movies = await movieService.GetPaginatableMovies(1, SearchText, SelectedSortType, SortDirection, vm.StartReleaseYear, vm.EndReleaseYear, vm.StartDuration, vm.EndDuration, vm.StartRating, vm.EndRating, vm.SelectedGenres);
            listFilm.Clear();
            foreach (var item in movies)
            {
                listFilm.Add(item);
            }
        }

        private void HandleSortDirectionButton()
        {
            SortDirection = 1 - SortDirection;
            HandleSearchButton();
        }

        private async Task HandleSearchButton()
        {
            List<Movie> tmpList = await movieService.GetPaginatableMovies(1, SearchText, SelectedSortType, SortDirection, vm.StartReleaseYear, vm.EndReleaseYear, vm.StartDuration, vm.EndDuration, vm.StartRating, vm.EndRating, vm.SelectedGenres);
            listFilm.Clear();
            foreach (var item in tmpList)
            {
                listFilm.Add(item);
            }
        }

        public double ActualWindowHeight
        {
            get { return _actualWindowHeight; }
            set
            {
                _actualWindowHeight = value;
                OnPropertyChanged(nameof(ActualWindowHeight));
            }
        }

        public ICommand SizeChangedCommand
        {
            get
            {
                if (_sizeChangedCommand == null)
                {
                    _sizeChangedCommand = new RelayCommand(param => OnSizeChanged());
                }
                return _sizeChangedCommand;
            }
            set
            {
                _sizeChangedCommand = value;
            }
        }

        private void OnSizeChanged()
        {
            // Xử lý logic khi kích thước thay đổi
        }
        public ObservableCollection<Movie> ListFilm
        {
            get { return listFilm; }
            set
            {
                listFilm = value;
                OnPropertyChanged(nameof(ListFilm));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
