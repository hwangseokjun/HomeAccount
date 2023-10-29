using HomeAccount.DataAccess;
using HomeAccount.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccount.ViewModels
{
    public interface ICategoryContext
    {
        IEnumerable ReadAll();
        object Save(string name);
        int Edit(object parameter);
        int Remove(object parameter);
    }

    public class CategoryContext<T> : ICategoryContext where T : CategoryBase, new()
    {
        private readonly IDataAccess<T> _dataAccess;

        public CategoryContext(IDataAccess<T> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable ReadAll()
        {
            return new ObservableCollection<T>(_dataAccess.ReadAll());
        }

        public object Save(string name)
        {
            T category = new T
            {
                Name = name
            };

            return _dataAccess.Add(category);
        }

        public int Edit(object parameter)
        {
            if (parameter is T category)
            {
                return _dataAccess.Modify(category);
            }

            return -1;
        }

        public int Remove(object parameter)
        {
            if (parameter is T category)
            {
                return _dataAccess.Remove(category);
            }

            return -1;
        }
    }
}
