using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeAccount.Utils
{
    public class ListBoxHelper
    {
        #region ItemDoubleClickCommand
        public static readonly DependencyProperty ItemDoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("ItemDoubleClickCommand", typeof(ICommand), typeof(ListBoxHelper), new PropertyMetadata(null, OnItemDoubleClickCommandPropertyChanged));

        public static ICommand GetItemDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ItemDoubleClickCommandProperty);
        }

        public static void SetItemDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ItemDoubleClickCommandProperty, value);
        }

        private static void OnItemDoubleClickCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBox listBox)
            {
                listBox.PreviewMouseDoubleClick -= ListBox_PreviewMouseDoubleClick;
                listBox.PreviewMouseDoubleClick += ListBox_PreviewMouseDoubleClick;
            }
        }

        private static void ListBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                ICommand command = GetItemDoubleClickCommand(listBox);
                command?.Execute(null);
            }
        }
        #endregion
    }
}
