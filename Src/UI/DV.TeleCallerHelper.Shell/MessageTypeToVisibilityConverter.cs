using DV.TeleCallerHelper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DV.TeleCallerHelper.Shell
{
    public class MessageTypeToVisibilityConverter : IValueConverter
    {
        public StatusMessageType MessageType
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StatusMessageType msgType;

            if (Enum.TryParse<StatusMessageType>(value.ToString(), out msgType))
            {
                if (msgType == MessageType)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
