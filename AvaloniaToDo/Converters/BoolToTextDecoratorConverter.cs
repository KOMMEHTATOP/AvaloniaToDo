using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Diagnostics;
using System.Globalization;

namespace AvaloniaToDo.Converters;

public class BoolToTextDecoratorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (bool)value ? TextDecorations.Strikethrough : null;
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.WriteLine("Rider не дает запушить без реализации этого метода");
        return null;
    }
}
