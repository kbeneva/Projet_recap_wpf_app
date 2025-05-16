using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using IdeaManager.Core.Enums;

namespace IdeaManager.UI.Converters
{
    public class PendingToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IdeaStatus status)
            {
                return status == IdeaStatus.Pending ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
