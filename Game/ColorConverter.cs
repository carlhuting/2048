using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;


namespace Game
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int num = 0;
            if (value is int)
                num = (int)value;
            else
            {
                string n = value.ToString();
                if (string.IsNullOrEmpty(n)) n = "0";
                num = System.Convert.ToInt32(n);
            }

            switch (num)
            {
                case 0:
                    return new SolidColorBrush(Color.FromArgb(255, 204, 192, 179));
                    break;
                case 2:
                    return new SolidColorBrush(Color.FromArgb(255, 98, 84, 153));
                    break;
                case 4:
                    return new SolidColorBrush(Color.FromArgb(255, 108, 84, 143));
                    break;
                case 8:
                    return new SolidColorBrush(Color.FromArgb(255, 118, 84, 133));
                    break;
                case 16:
                    return new SolidColorBrush(Color.FromArgb(255, 128, 84, 123));
                    break;
                case 32:
                    return new SolidColorBrush(Color.FromArgb(255, 138, 84, 113));
                    break;
                case 64:
                    return new SolidColorBrush(Color.FromArgb(255, 148, 84, 103));
                    break;
                case 128:
                    return new SolidColorBrush(Color.FromArgb(255, 158, 84, 93));
                    break;
                case 512:
                    return new SolidColorBrush(Color.FromArgb(255, 168, 84, 93));
                    break;
                default:
                    return new SolidColorBrush(Color.FromArgb(255, 178, 84, 63));
                    break;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
