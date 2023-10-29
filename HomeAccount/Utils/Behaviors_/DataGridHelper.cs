using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeAccount.Utils
{
    public class DataGridHelper
    {
        #region CommitCommand
        public static readonly DependencyProperty CommitCommandProperty =
            DependencyProperty.RegisterAttached("CommitCommand", typeof(ICommand), typeof(DataGridHelper), new PropertyMetadata(null));

        public static ICommand GetCommitCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommitCommandProperty);
        }

        public static void SetCommitCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommitCommandProperty, value);
        }
        #endregion

        #region SyncToDatabase
        public static readonly DependencyProperty SyncToDatabaseProperty =
            DependencyProperty.RegisterAttached("SyncToDatabase", typeof(bool), typeof(DataGridHelper), new PropertyMetadata(false, OnSyncToDatabasePropertyChanged));

        public static bool GetSyncToDatabase(DependencyObject obj)
        {
            return (bool)obj.GetValue(SyncToDatabaseProperty);
        }

        public static void SetSyncToDatabase(DependencyObject obj, bool value)
        {
            obj.SetValue(SyncToDatabaseProperty, value);
        }

        private static void OnSyncToDatabasePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                dataGrid.SourceUpdated += DataGrid_SourceUpdated;
            }
        }

        private static void DataGrid_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.GetValue(CommitCommandProperty) is ICommand commitCommand)
            {
                commitCommand?.Execute(null);
            }
        }
        #endregion

        #region DoubleClickCommand
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(DataGridHelper), new PropertyMetadata(null, OnDoubleClickCommandPropertyChanged));

        public static ICommand GetDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DoubleClickCommandProperty);
        }

        public static void SetDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }

        private static void OnDoubleClickCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                dataGrid.PreviewMouseDoubleClick -= DataGrid_PreviewMouseDoubleClick;
                dataGrid.PreviewMouseDoubleClick += DataGrid_PreviewMouseDoubleClick;
            }
        }

        private static void DataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid && e.OriginalSource is TextBlock)
            {
                ICommand command = GetDoubleClickCommand(dataGrid);
                command?.Execute(null);
            }
        }
        #endregion

        #region SelectedItems
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IEnumerable), typeof(DataGridHelper), new PropertyMetadata(null, OnSelectedItemsPropertyChanged));

        public static IEnumerable GetSelectedItems(DependencyObject obj)
        {
            return (IEnumerable)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IEnumerable value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        private static void OnSelectedItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine(e.NewValue);
        }
        #endregion

        #region UseSelectedItems
        public static readonly DependencyProperty UseSelectedItemsProperty =
            DependencyProperty.RegisterAttached("UseSelectedItems", typeof(bool), typeof(DataGridHelper), new PropertyMetadata(false, OnUseSelectedItemsChanged));

        public static bool GetUseSelectedItems(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseSelectedItemsProperty);
        }

        public static void SetUseSelectedItems(DependencyObject obj, bool value)
        {
            obj.SetValue(UseSelectedItemsProperty, value);
        }

        private static void OnUseSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                dataGrid.SelectedCellsChanged += DataGrid_SelectedCellsChanged;
            }
        }

        private static void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                SetSelectedItems(dataGrid, dataGrid.SelectedItems);
            }
        }
        #endregion
    }
}