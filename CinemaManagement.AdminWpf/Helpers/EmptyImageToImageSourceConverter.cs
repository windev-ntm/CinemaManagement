using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CinemaManagement.AdminWpf.Helpers
{
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class EmptyImageToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts an empty string value into the DefaultImagePath property value if it exists, or a DependencyProperty.UnsetValue otherwise.
        /// </summary>
        public string DefaultImagePath { get; set; } = string.Empty;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource)) return DependencyProperty.UnsetValue;

            if (value is not string && value != null)
            {
                throw new ArgumentException("The value must be a string");
            }

            var imagePath = value as string;

            return string.IsNullOrEmpty(imagePath) ? string.IsNullOrEmpty(DefaultImagePath) ? DependencyProperty.UnsetValue : DefaultImagePath : imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
