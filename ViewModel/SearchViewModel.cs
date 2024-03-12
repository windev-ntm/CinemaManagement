using CinemaManagement.Models;
using CinemaManagement.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace CinemaManagement
{
    internal class SearchViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Movie> listFilm;

        private MovieService movieService;

        private ICommand _sizeChangedCommand, _searchCommand, _sortDirectionCommand;
        private double _actualWindowHeight;

        private string _searchText;

        private string _selectedSortType;
        private List<string> _sortType;

        public SearchViewModel()
        {

            SortDirection = 0;
            _sortType = new List<string> { "IMDB Rating", "Release Date", "Alphabetical", "Runtime" };
            movieService = new MovieService();
            listFilm = new ObservableCollection<Movie>(movieService.GetMovies());

            for (int i = 0; i < listFilm.Count; i++)
            {
                Debug.WriteLine(listFilm[i].Name);
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

        private void HandleSortDirectionButton()
        {
            SortDirection = 1 - SortDirection;
            HandleSearchButton();
        }

        private void HandleSearchButton()
        {
            List<Movie> tmpList = movieService.GetMovieBySearch(SearchText, SelectedSortType, SortDirection);
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
