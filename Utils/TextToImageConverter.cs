using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Tic_tac_Toe_WPF.Utils
{
    public class TextToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "X":
                    return new BitmapImage(new Uri("Assets/cross.png", UriKind.Relative));
                case "O":
                    return new BitmapImage(new Uri("Assets/circle.png", UriKind.Relative));
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
