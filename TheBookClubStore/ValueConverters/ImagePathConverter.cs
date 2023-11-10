using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheBookClubStore.ValueConverters;

public class ImagePathConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter != null && parameter.ToString() == "User")
        {
            return "../Resources/UserPhotos/" + value;
        }

        if (parameter != null && parameter.ToString() == "Product")
        {
            return "../Resources/ProductPhotos/" + value;
        }
        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}