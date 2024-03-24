// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Globalization;
using System.Windows.Data;
using Wpf.Ui.Appearance;

namespace CinemaManagement.AdminWpf.Helpers
{
    internal class ApplicationThemeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not String enumString)
            {
                throw new ArgumentException("ExceptionApplicationThemeToBooleanConverterParameterMustBeAnEnumName");
            }

            if (!Enum.IsDefined(typeof(ApplicationTheme), value))
            {
                throw new ArgumentException("ExceptionApplicationThemeToBooleanConverterValueMustBeAnEnum");
            }

            var enumValue = Enum.Parse(typeof(ApplicationTheme), enumString);

            return enumValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not String enumString)
            {
                throw new ArgumentException("ExceptionApplicationThemeToBooleanConverterParameterMustBeAnEnumName");
            }

            return Enum.Parse(typeof(ApplicationTheme), enumString);
        }
    }
}
