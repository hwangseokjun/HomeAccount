using HomeAccount.Commands;
using HomeAccount.DataAccess;
using HomeAccount.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeAccount.ViewModels
{
    public class IncomeViewModel : ViewModelBase
    {
        public event Action OnClosed;
        public event Action<Finance> OnSaved;
        private readonly IFinanceDataAccess<Finance> _financeDB;
        private readonly IDataAccess<IncomeCategory> _categoryDB;
        private readonly IDataAccess<IncomeMethod> _methodDB;
        private readonly IDataAccess<IncomeSource> _sourceDB;
        private int _amount;
        private DateTime _date;
        private string _note;
        private IncomeCategory _selectedCategory;
        private IncomeMethod _selectedMethod;
        private IncomeSource _selectedSource;

        public int Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }
        public IncomeCategory SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }
        public IncomeMethod SelectedMethod
        {
            get => _selectedMethod;
            set => SetProperty(ref _selectedMethod, value);
        }
        public IncomeSource SelectedSource
        {
            get => _selectedSource;
            set => SetProperty(ref _selectedSource, value);
        }
        public ObservableCollection<IncomeCategory> Categories { get; set; }
        public ObservableCollection<IncomeMethod> Methods { get; set; }
        public ObservableCollection<IncomeSource> Sources { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public IncomeViewModel(
            IFinanceDataAccess<Finance> financeDB,
            IDataAccess<IncomeCategory> categoryDB,
            IDataAccess<IncomeMethod> methodDB,
            IDataAccess<IncomeSource> sourceDB)
        {
            _financeDB = financeDB;
            _categoryDB = categoryDB;
            _methodDB = methodDB;
            _sourceDB = sourceDB;
            IEnumerable<IncomeCategory> categories = _categoryDB.ReadAll();
            IEnumerable<IncomeMethod> methods = _methodDB.ReadAll();
            IEnumerable<IncomeSource> sources = _sourceDB.ReadAll();
            Categories = new ObservableCollection<IncomeCategory>(categories);
            Methods = new ObservableCollection<IncomeMethod>(methods);
            Sources = new ObservableCollection<IncomeSource>(sources);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CloseCommand = new RelayCommand(ExecuteClose);
        }

        private void ExecuteSave(object parameter)
        {
            var income = new Finance
            {
                Amount = _amount,
                Date = _date,
                Note = _note,
                Category = _selectedCategory,
                Source = _selectedSource,
                Method = _selectedMethod
            };
            income = _financeDB.Add(income);

            OnSaved?.Invoke(income);
        }

        private bool CanExecuteSave(object parameter)
        {
            return true;
        }

        private void ExecuteClose(object parameter)
        {
            OnClosed?.Invoke();
        }
    }
}