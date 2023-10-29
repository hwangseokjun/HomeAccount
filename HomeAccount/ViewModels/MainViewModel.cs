using HomeAccount.Commands;
using HomeAccount.DataAccess;
using HomeAccount.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeAccount.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DashboardViewModel _dashboardView;
        private readonly FinanceViewModel _financeView;
        private readonly TaskViewModel _taskView;
        private INotifyPropertyChanged _currentContents;

        public INotifyPropertyChanged CurrentContents
        {
            get => _currentContents;
            set => SetProperty(ref _currentContents, value);
        }

        public ICommand DashboardCommand { get; private set; }
        public ICommand FinanceCommand { get; private set; }
        public ICommand TaskCommand { get; private set; }

        public MainViewModel()
        {
            var financeDB = new FinanceDB();
            _dashboardView = new DashboardViewModel();
            _financeView = new FinanceViewModel(financeDB);
            _taskView = new TaskViewModel();
            CurrentContents = _dashboardView;
            DashboardCommand = new RelayCommand(ExecuteDashboard);
            FinanceCommand = new RelayCommand(ExecuteFinance);
            TaskCommand = new RelayCommand(ExecuteTask);
        }
        
        private void ExecuteDashboard(object parameter)
        {
            CurrentContents = _dashboardView;
        }
        
        private void ExecuteFinance(object parameter)
        {
            CurrentContents = _financeView;
        }
        
        private void ExecuteTask(object parameter)
        {
            CurrentContents = _taskView;
        }
    }
}