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
    public class ExpenseViewModel : ViewModelBase
    {
        public event Action OnClosed;
        public event Action<Finance> OnSaved;
        private readonly IFinanceDataAccess<Finance> _financeDB;
        private readonly IDataAccess<ExpenseCategory> _categoryDB;
        private readonly IDataAccess<ExpenseMethod> _methodDB;
        private readonly IDataAccess<ExpenseSource> _sourceDB;
        private int _amount;
        private DateTime _date;
        private string _note;
        private ExpenseCategory _selectedCategory;
        private ExpenseMethod _selectedMethod;
        private ExpenseSource _selectedSource;

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
        public ExpenseCategory SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }
        public ExpenseMethod SelectedMethod
        {
            get => _selectedMethod;
            set => SetProperty(ref _selectedMethod, value);
        }
        public ExpenseSource SelectedSource
        {
            get => _selectedSource;
            set => SetProperty(ref _selectedSource, value);
        }
        public ObservableCollection<ExpenseCategory> Categories { get; set; }
        public ObservableCollection<ExpenseMethod> Methods { get; set; }
        public ObservableCollection<ExpenseSource> Sources { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public ExpenseViewModel(
            IFinanceDataAccess<Finance> financeDB,
            IDataAccess<ExpenseCategory> categoryDB,
            IDataAccess<ExpenseMethod> methodDB,
            IDataAccess<ExpenseSource> sourceDB)
        {
            _financeDB = financeDB;
            _categoryDB = categoryDB;
            _methodDB = methodDB;
            _sourceDB = sourceDB;
            IEnumerable<ExpenseCategory> categories = _categoryDB.ReadAll();
            IEnumerable<ExpenseMethod> methods = _methodDB.ReadAll();
            IEnumerable<ExpenseSource> sources = _sourceDB.ReadAll();
            Categories = new ObservableCollection<ExpenseCategory>(categories);
            Methods = new ObservableCollection<ExpenseMethod>(methods);
            Sources = new ObservableCollection<ExpenseSource>(sources);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CloseCommand = new RelayCommand(ExecuteClose);
        }

        private void ExecuteSave(object parameter)
        {
            var expense = new Finance
            {
                Amount = _amount,
                Date = _date,
                Note = _note,
                Category = _selectedCategory,
                Source = _selectedSource,
                Method = _selectedMethod
            };
            expense = _financeDB.Add(expense);

            OnSaved?.Invoke(expense);
        }

        private bool CanExecuteSave(object parameter)
        {
            // 실행 가능한지 여부를 결정하는 로직을 구현합니다.
            return true;
        }

        private void ExecuteClose(object parameter)
        {
            OnClosed?.Invoke();
        }
    }
}