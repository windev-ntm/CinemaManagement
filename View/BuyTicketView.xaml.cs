using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for BuyTicketView.xaml
    /// </summary>
    public partial class BuyTicketView : Window
    {
        public List<string> Sample= new List<string>();
        public BuyTicketView()
        {
            InitializeComponent();
            DataContext = Sample;
            for(int i=1;i<=100;i++)
            {
                Sample.Add(i.ToString());
            }
        }
    }
}
