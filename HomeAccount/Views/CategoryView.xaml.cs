using HomeAccount.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeAccount.Views
{
    /// <summary>
    /// CategoryView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CategoryView : UserControl
    {
        public CategoryView()
        {
            InitializeComponent();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = listBox.SelectedIndex;

            if (0 < selectedIndex && listBox.ItemsSource is ObservableCollection<CategoryBase> items)
            {
                var itemToMoveUp = items[selectedIndex];
                items.RemoveAt(selectedIndex);
                items.Insert(selectedIndex - 1, itemToMoveUp);
                listBox.SelectedIndex = selectedIndex - 1;
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = listBox.SelectedIndex;

            if (selectedIndex + 1 < listBox.Items.Count && listBox.ItemsSource is ObservableCollection<CategoryBase> items)
            {
                var itemToMoveUp = items[selectedIndex];
                items.RemoveAt(selectedIndex);
                items.Insert(selectedIndex + 1, itemToMoveUp);
                listBox.SelectedIndex = selectedIndex + 1;
            }
        }
    }
}