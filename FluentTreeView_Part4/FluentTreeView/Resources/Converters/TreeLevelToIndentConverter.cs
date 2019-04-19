using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FluentTreeView.Resources.Converters
{
    public class TreeLevelToIndentConverter : IValueConverter
    {
        public Thickness Margin { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeViewItem ti)
            {
                int level = 0;
                FrameworkElement current = ti;
                do
                {
                    if(VisualTreeHelper.GetParent(current) is FrameworkElement fe)
                    {
                        if (fe is TreeViewItem)
                        {
                            level++;
                        }
                        if (fe is TreeView)
                        {
                            break;
                        }
                        current = fe;
                    }
                } while (current!=null);
                return new Thickness(Margin.Left * level, 0, Margin.Right * level, 0);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
