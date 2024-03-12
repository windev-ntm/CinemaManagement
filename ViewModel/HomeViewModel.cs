using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel
{
    class HomeViewModel
    {
        public string findImage { get; set; }
        public string logoPath { get; set; }
        public string filmImage { get; set; }

        public HomeViewModel()
        {
            findImage = Directory.GetCurrentDirectory() + "\\find.png";
            logoPath = Directory.GetCurrentDirectory() + "\\logo.png";
            filmImage = Directory.GetCurrentDirectory() + "\\film1.jpg";
        }
    }
}
