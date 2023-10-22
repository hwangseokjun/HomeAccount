using HomeAccount.Commands;
using HomeAccount.DataAccess;
using HomeAccount.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeAccount.ViewModels
{
    public class FinanceViewModel : ViewModelBase
    {
        private readonly IFinanceDataAccess<Finance> _database;
        private int _payment;
        private int _expense;
        private int _total;
        private Finance _selectedFinance;
        private DateTime _start;
        private DateTime _end;
        private bool _showIncomeView;
        private bool _showExpenseView;
        private bool _showEditView;
        private bool _isPaymentChecked = true;
        private bool _isExpenseChecked;

        public DateTime Start
        {
            get => _start;
            set => SetProperty(ref _start, value);
        }
        public DateTime End
        {
            get => _end;
            set => SetProperty(ref _end, value);
        }
        public int Payment
        {
            get => _payment;
            set => SetProperty(ref _payment, value);
        }
        public int Expense
        {
            get => _expense;
            set => SetProperty(ref _expense, value);
        }
        public int Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }
        public Finance SelectedFinance
        {
            get => _selectedFinance;
            set => SetProperty(ref _selectedFinance, value);
        }
        public FinanceCollection FinanceCollection { get; set; }
        public bool ShowIncomeView
        {
            get => _showIncomeView;
            set => SetProperty(ref _showIncomeView, value);
        }
        public bool ShowExpenseView
        {
            get => _showExpenseView;
            set => SetProperty(ref _showExpenseView, value);
        }
        public bool ShowEditView
        {
            get => _showEditView;
            set => SetProperty(ref _showEditView, value);
        }
        public bool IsPaymentChecked
        {
            get => _isPaymentChecked;
            set => SetProperty(ref _isPaymentChecked, value);
        }
        public bool IsExpenseChecked
        {
            get => _isExpenseChecked;
            set => SetProperty(ref _isExpenseChecked, value);
        }
        public IncomeViewModel IncomeView { get; set; }
        public ExpenseViewModel ExpenseView { get; set; }
        public EditViewModel EditView { get; set; }

        public ICommand ShowIncomeViewCommand { get; private set; }
        public ICommand ShowExpenseViewCommand { get; private set; }
        public ICommand ShowEditViewCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand ExportToExcelCommand { get; private set; }

        public FinanceViewModel(IFinanceDataAccess<Finance> database)
        {
            _database = database;
            FinanceCollection = new FinanceCollection();
            FinanceCollection.Finances.CollectionChanged += Finances_CollectionChanged;
            FinanceCollection.Finances.Add(new Finance { Amount = 10000 });
            FinanceCollection.Finances.Add(new Finance { Amount = -10000 });
            var incomeCategoryDB = new IncomeCategoryDB();
            var incomeMethodDB = new IncomeMethodDB();
            var incomeSourceDB = new IncomeSourceDB();
            IncomeView = new IncomeViewModel(_database, incomeCategoryDB, incomeMethodDB, incomeSourceDB);
            var expenseCategoryDB = new ExpenseCategoryDB();
            var expenseMethodDB = new ExpenseMethodDB();
            var expenseSourceDB = new ExpenseSourceDB();
            ExpenseView = new ExpenseViewModel(_database, expenseCategoryDB, expenseMethodDB, expenseSourceDB);
            EditView = new EditViewModel(_database);
            IncomeView.OnClosed += IncomeView_OnClosed;
            IncomeView.OnSaved += IncomeView_OnSaved;
            ExpenseView.OnClosed += ExpenseView_OnClosed;
            ExpenseView.OnSaved += ExpenseView_OnSaved;
            EditView.OnClosed += EditView_OnClosed;
            ShowIncomeViewCommand = new RelayCommand(ExecuteShowIncomeView);
            ShowExpenseViewCommand = new RelayCommand(ExecuteShowExpenseView);
            ShowEditViewCommand = new RelayCommand(ExecuteShowEditView, CanExecuteShowEditView);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            SearchCommand = new RelayCommand(ExecuteSearch, CanExecuteSearch);
            ExportToExcelCommand = new RelayCommand(ExecuteExportToExcel, CanExecuteExportToExcel);
        }

        private void Finances_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int payment = 0;
            int expense = 0;

            foreach (var finance in FinanceCollection.Finances)
            {
                int amount = finance.Amount;

                if (0 < amount)
                {
                    payment += amount;
                }
                else if (amount < 0)
                {
                    expense -= amount;
                }
            }

            Payment = payment;
            Expense = expense;
            Total = payment - expense;
        }

        private void IncomeView_OnClosed()
        {
            ShowIncomeView = false;
        }

        private void IncomeView_OnSaved(Finance obj)
        {
            FinanceCollection.Finances.Add(obj);
        }

        private void ExpenseView_OnClosed()
        {
            ShowExpenseView = false;
        }

        private void ExpenseView_OnSaved(Finance obj)
        {
            FinanceCollection.Finances.Add(obj);
        }

        private void EditView_OnClosed()
        {
            ShowEditView = false;
        }

        private void ExecuteShowIncomeView(object parameter)
        {
            ShowIncomeView = true;
        }

        private void ExecuteShowExpenseView(object parameter)
        {
            ShowExpenseView = true;
        }

        private void ExecuteShowEditView(object parameter)
        {
            EditView.Finance = SelectedFinance;
            ShowEditView = true;
        }

        private bool CanExecuteShowEditView(object parameter)
        {
            return _selectedFinance != null;
        }

        private void ExecuteDelete(object parameter)
        {
            Console.WriteLine($"{_selectedFinance}");
        }

        private bool CanExecuteDelete(object parameter)
        {
            return _selectedFinance != null;
        }

        private void ExecuteSearch(object parameter)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteSearch(object parameter)
        {
            // 실행 가능한지 여부를 결정하는 로직을 구현합니다.
            return true;
        }

        private void ExecuteExportToExcel(object parameter)
        {
            FinanceCollection.ExportToExcel();
        }

        private bool CanExecuteExportToExcel(object parameter)
        {
            return FinanceCollection.Finances != null && 0 < FinanceCollection.Finances.Count;
        }
    }
}