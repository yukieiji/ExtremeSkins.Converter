using System.Windows.Controls;
using System.Windows;

namespace ExtremeSkins.Converter.Behavior;

public sealed class ScrollToBottomBehavior
{
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.RegisterAttached(
            "IsEnabled", typeof(bool),
            typeof(ScrollToBottomBehavior),
        new PropertyMetadata(false, OnIsEnabledPropertyChanged));

    public static bool GetIsEnabled(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsEnabledProperty);
    }

    public static void SetIsEnabled(DependencyObject obj, bool value)
    {
        obj.SetValue(IsEnabledProperty, value);
    }

    private static void OnIsEnabledPropertyChanged(
        DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        var textBox = obj as TextBox;

        if (textBox != null && args.NewValue is bool && (bool)args.NewValue)
        {
            textBox.Loaded += TextBox_Loaded;
            textBox.TextChanged += TextBox_TextChanged;
        }
        else if (textBox != null)
        {
            textBox.Loaded -= TextBox_Loaded;
            textBox.TextChanged -= TextBox_TextChanged;
        }
    }

    private static void TextBox_Loaded(object sender, RoutedEventArgs e)
    {
        var textBox = sender as TextBox;
        textBox.ScrollToEnd();
    }

    private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        textBox.ScrollToEnd();
    }
}
