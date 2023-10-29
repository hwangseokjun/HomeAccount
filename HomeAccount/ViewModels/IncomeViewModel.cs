using HomeAccount.Commands;
using HomeAccount.DataAccess;
using HomeAccount.Models;
using System;
using System.Collections;
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
        public CategoryContext<IncomeCategory> IncomeCategory { get; set; }
        public CategoryContext<IncomeMethod> IncomeMethod { get; set; }
        public CategoryContext<IncomeSource> IncomeSource { get; set; }
        public CategoryViewModel CategoryViewModel { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand ShowCategoryCommand { get; private set; }
        public ICommand ShowMethodCommand { get; private set; }
        public ICommand ShowSourceCommand { get; private set; }

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
            CategoryViewModel = new CategoryViewModel();
            CategoryViewModel.OnCategoryChanged += CategoryViewModel_OnCategoryChanged;
            IncomeCategory = new CategoryContext<IncomeCategory>(_categoryDB);
            IncomeMethod = new CategoryContext<IncomeMethod>(_methodDB);
            IncomeSource = new CategoryContext<IncomeSource>(_sourceDB);
            Categories = new ObservableCollection<IncomeCategory>(categories);
            Methods = new ObservableCollection<IncomeMethod>(methods);
            Sources = new ObservableCollection<IncomeSource>(sources);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            ShowCategoryCommand = new RelayCommand(ExecuteShowCategory);
            ShowMethodCommand = new RelayCommand(ExecuteShowMethod);
            ShowSourceCommand = new RelayCommand(ExecuteShowSource);
            CloseCommand = new RelayCommand(ExecuteClose);
        }

        // TODO: 최적화하기
        private void CategoryViewModel_OnCategoryChanged()
        {
            Categories.Clear();
            Methods.Clear();
            Sources.Clear();
            IEnumerable<IncomeCategory> categories = _categoryDB.ReadAll();
            IEnumerable<IncomeMethod> methods = _methodDB.ReadAll();
            IEnumerable<IncomeSource> sources = _sourceDB.ReadAll();

            foreach (var category in categories) 
            {
                Categories.Add(category);
            }

            foreach (var method in methods)
            {
                Methods.Add(method);
            }

            foreach (var source in sources)
            {
                Sources.Add(source);
            }
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
            if (CategoryViewModel.ShowEdit)
            {
                CategoryViewModel.ShowEdit = false;
                return;
            }

            if (CategoryViewModel.Show)
            {
                CategoryViewModel.Show = false;
                return;
            }
            
            OnClosed?.Invoke();
        }

        private void ExecuteShowCategory(object parameter)
        {
            CategoryViewModel.Header = "수입 카테고리";
            CategoryViewModel.CategoryContext = IncomeCategory;
            CategoryViewModel.Show = true;
        }

        private void ExecuteShowMethod(object parameter)
        {
            CategoryViewModel.Header = "수입처";
            CategoryViewModel.CategoryContext = IncomeMethod;
            CategoryViewModel.Show = true;
        }

        private void ExecuteShowSource(object parameter)
        {
            CategoryViewModel.Header = "수입 방법";
            CategoryViewModel.CategoryContext = IncomeSource;
            CategoryViewModel.Show = true;
        }
    }
}