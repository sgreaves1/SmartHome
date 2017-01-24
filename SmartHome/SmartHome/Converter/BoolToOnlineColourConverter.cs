﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SmartHome.Converter
{
    public class BoolToOnlineColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value)
            {
                return Brushes.Green;
            }

            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
