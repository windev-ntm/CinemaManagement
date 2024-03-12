using CinemaManagement.AdminWpf.ViewModels.Controls;
using CinemaManagement.Data.Services;

namespace CinemaManagement.AdminWpf.ViewModels.Pages
{
    public partial class MoviesViewModel : ObservableObject
    {
        private readonly MoviesService _moviesService;
        private readonly MovieListViewModel _movieListViewModel;
        private Type _currentViewModelType;

        public ObservableObject CurrentViewModel
        {
            get
            {
                if (_currentViewModelType == typeof(MovieListViewModel))
                {
                    return _movieListViewModel;
                }
                else
                {
                    throw new Exception("Invalid ViewModel type");
                }
            }
        }

        public MoviesViewModel(MoviesService moviesService)
        {
            _movieListViewModel = new MovieListViewModel(moviesService);
            _currentViewModelType = typeof(MovieListViewModel);
            _moviesService = moviesService;
        }

        //public void OnNavigatedTo()
        //{
        //    _movieListViewModel.DoSomethingCommand.Execute(null);

        //    //throw new NotImplementedException();
        //}

        //public void OnNavigatedFrom()
        //{
        //    //throw new NotImplementedException();
        //}
    }
}
