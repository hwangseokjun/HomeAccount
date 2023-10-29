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
    public class GridHelper
    {
        #region Command
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(GridHelper), new PropertyMetadata(null, OnCommandPropertyChanged));

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        private static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Grid grid)
            {
                grid.MouseDown -= Grid_MouseDown;
                grid.MouseDown += Grid_MouseDown;
            }
        }

        private static void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid && grid.GetValue(CommandProperty) is ICommand command)
            {
                command?.Execute(null);
            }
        }
        #endregion
    }
}
