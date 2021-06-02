using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BongoBlaster
{
	public class BooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try
            {
				Boolean isVisible = (Boolean)value;
				if (isVisible)
                {
					return Visibility.Visible;
                }
				else
                {
					return Visibility.Hidden;
                }
            }
			catch
            {
				return Visibility.Hidden;
            }
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try
            {
				Visibility visibility = (Visibility)value;
				if (visibility == Visibility.Visible)
                {
					return true;
                }
				else
                {
					return false;
                }
            }
			catch
            {
				return false;
            }
		}
	}

	public class BooleanToForegroundBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try
			{
				Boolean isVisible = (Boolean)value;
				if (isVisible)
				{
					return Brushes.White;
				}
				else
				{
					return Brushes.Black;
				}
			}
			catch
			{
				return Brushes.Black;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try
			{
				Brush brush = (Brush)value;
				if (brush == Brushes.White)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}
	}
}
