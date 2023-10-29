using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HomeAccount.Utils
{
    public class TextBoxHelper
    {
        #region IsNumberOnly
        private static Regex _numberOnly = new Regex(@"^-?[0-9][0-9,\.]+$");
        public static readonly DependencyProperty IsNumberOnlyProperty =
            DependencyProperty.RegisterAttached("IsNumberOnly", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false, OnIsNumberOnlyPropertyChanged));

        public static bool GetIsNumberOnly(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumberOnlyProperty);
        }

        public static void SetIsNumberOnly(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumberOnlyProperty, value);
        }

        private static void OnIsNumberOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.PreviewTextInput += TextBox_PreviewTextInput;
            }
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!_numberOnly.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region UseOnPropertyChanged
        public static readonly DependencyProperty UseOnPropertyChangedProperty =
            DependencyProperty.RegisterAttached("UseOnPropertyChanged", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false, OnUseOnPropertyChanged));

        public static bool GetUseOnPropertyChanged(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseOnPropertyChangedProperty);
        }

        public static void SetUseOnPropertyChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(UseOnPropertyChangedProperty, value);
        }

        private static void OnUseOnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;

            if (textBox != null)
            {
                textBox.TextChanged += TextBox_TextChanged;
            }
            else
            {
                textBox.TextChanged -= TextBox_TextChanged;
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox) 
            {
                BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
                bindingExpression?.UpdateSource();
            }
        }
        #endregion
    }
}