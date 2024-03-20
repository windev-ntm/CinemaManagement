using CinemaManagement.Models;
using CinemaManagement.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
namespace CinemaManagement.ViewModel
{

    public class ButtonModel : INotifyPropertyChanged
    {
        public int dem = 0;

        private bool _isSelected;
        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public ButtonModel()
        {
            IsSelected = false;

        }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                dem++;
                if (dem % 2 == 1)
                {
                    _isSelected = true;
                }
                else
                {
                    _isSelected = false;
                }
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class FilterSubScreenViewModel : INotifyPropertyChanged
    {


        public FilterSubScreenViewModel(FilterSubScreenViewModel filterSubScreenViewModel)
        {
            genreService = new GenreService();
            ButtonList = filterSubScreenViewModel.ButtonList;
            _startDuration = filterSubScreenViewModel.StartDuration;
            _endDuration = filterSubScreenViewModel.EndDuration;
            _startRating = filterSubScreenViewModel.StartRating;
            _endRating = filterSubScreenViewModel.EndRating;
            _startReleaseYear = filterSubScreenViewModel.StartReleaseYear;
            _endReleaseYear = filterSubScreenViewModel.EndReleaseYear;
            //LoadButtonList();
            // Thêm các mục khác tương tự ở đây
        }

        public FilterSubScreenViewModel()
        {
            genreService = new GenreService();
            ButtonList = new ObservableCollection<ButtonModel>();
            _startDuration = 0;
            _endDuration = 1000;
            _startRating = 0;
            _endRating = 10;
            _startReleaseYear = 0;
            _endReleaseYear = 100000;



            LoadButtonList();
            // Thêm các mục khác tương tự ở đây
        }
        private async Task LoadButtonList()
        {

            List<Genre> genres;
            genres = await genreService.GetGenres();
            foreach (var item in genres)
            {
                ButtonList.Add(new ButtonModel() { Id = item.Id, Content = item.Name, IsSelected = false });
            }

        }

        private void HandleResetButton()
        {
            StartReleaseYear = 0;
            EndReleaseYear = 0;
            StartDuration = 0;
            EndDuration = 0;
            StartRating = 0;
            EndRating = 0;
            if (ButtonList.Count > 0)
            {
                foreach (var item in ButtonList)
                {
                    item.dem = -1;
                    item.IsSelected = false;
                }
            }
        }

        private void HandleCloseButton()
        {
            HandleResetButton();
            CloseAction();
        }

        private void HandleApplyButton()
        {
            SelectedGenres = new List<int>();

            for (int i = 0; i < ButtonList.Count; i++)
            {
                if (ButtonList[i].IsSelected)
                {
                    SelectedGenres.Add(ButtonList[i].Id);
                }
            }
            CloseAction();

            // Xử lý logic khi nhấn nút Apply
        }


        private ObservableCollection<ButtonModel> _buttonList;
        private List<Genre> _genres;
        private GenreService genreService;
        private ICommand _applyButton;
        private ICommand _resetButton;
        private ICommand _closeButton;
        private int _startReleaseYear;
        private int _endReleaseYear;
        private int _startDuration;
        private int _endDuration;
        private double _startRating;
        private double _endRating;

        public List<int> SelectedGenres;

        public Action CloseAction { get; set; }


        public ICommand CloseButton
        {
            get
            {
                if (_closeButton == null)
                {
                    _closeButton = new RelayCommand(param => HandleCloseButton());
                }
                return _closeButton;
            }
        }
        public double EndRating
        {
            get { return _endRating; }
            set
            {
                _endRating = value;
                OnPropertyChanged(nameof(EndRating));
            }
        }
        public double StartRating
        {
            get { return _startRating; }
            set
            {
                _startRating = value;
                OnPropertyChanged(nameof(StartRating));
            }
        }


        public int EndDuration
        {
            get { return _endDuration; }
            set
            {
                _endDuration = value;
                OnPropertyChanged(nameof(EndDuration));
            }
        }

        public int StartDuration
        {
            get { return _startDuration; }
            set
            {
                _startDuration = value;
                OnPropertyChanged(nameof(StartDuration));
            }
        }

        public int StartReleaseYear
        {
            get { return _startReleaseYear; }
            set
            {
                _startReleaseYear = value;
                OnPropertyChanged(nameof(StartReleaseYear));
            }
        }

        public int EndReleaseYear
        {
            get { return _endReleaseYear; }
            set
            {
                _endReleaseYear = value;
                OnPropertyChanged(nameof(EndReleaseYear));
            }
        }

        public ICommand ResetButton
        {
            get
            {
                if (_resetButton == null)
                {
                    _resetButton = new RelayCommand(param => HandleResetButton());
                }
                return _resetButton;
            }
        }

        public ICommand ApplyButton
        {
            get
            {
                if (_applyButton == null)
                {
                    _applyButton = new RelayCommand(param => HandleApplyButton());
                }
                return _applyButton;
            }
        }

        public ObservableCollection<ButtonModel> ButtonList
        {
            get { return _buttonList; }
            set
            {
                _buttonList = value;
                OnPropertyChanged(nameof(ButtonList));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
