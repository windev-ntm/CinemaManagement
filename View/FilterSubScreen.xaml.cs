using CinemaManagement.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for FilterSubScreen.xaml
    /// </summary>
    public partial class FilterSubScreen : Window
    {
        public FilterSubScreen(FilterSubScreenViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new System.Action(this.Close);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                // Cho phép di chuyển cửa sổ khi click trái chuột
                this.DragMove();
            }
        }
    }
}
