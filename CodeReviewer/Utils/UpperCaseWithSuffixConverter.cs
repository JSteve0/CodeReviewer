using System.Globalization;
using System.Windows.Data;

namespace CodeReviewer.Utils;

public class UpperCaseWithSuffixConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is string str) {
            if (parameter is string suffix) return str + suffix;
            return str;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
