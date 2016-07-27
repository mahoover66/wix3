using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Microsoft.Tools.WindowsInstallerXml.Tools
{
    public class NameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            if (null == input)
            {
                return null;
            }

            Color color = Color.FromName(input);

            return new SolidBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidBrush brush = value as SolidBrush;
            if (null == brush)
            {
                return null;
            }

            return brush.Color.Name;
        }
    }
}
