using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CinemaManagement.AdminWpf.Helpers
{
    class UrlToBitmapImageConverter : IValueConverter
    {

        public string DefaultImagePath { get; set; } = string.Empty;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string url)
            {
                throw new InvalidOperationException("Input value is not a string");
            }

            try
            {
                BitmapImage bmi = new();
                bmi.BeginInit();
                bmi.UriSource = new Uri(url);
                bmi.EndInit();

                return bmi;
            }
            catch (Exception e)
            {
                if (string.IsNullOrEmpty(DefaultImagePath))
                    throw new InvalidOperationException("Failed to convert URL to BitmapImage", e);

                BitmapImage bmi = new();
                bmi.BeginInit();
                bmi.UriSource = new Uri(DefaultImagePath);
                bmi.EndInit();

                return bmi;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
