using HomeAccount.Commands;
using HomeAccount.DataAccess;
using HomeAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeAccount.ViewModels
{
    public class EditViewModel : ViewModelBase
    {
        private readonly IFinanceDataAccess<Finance> _database;
        private bool _show;
        private Finance _finance;

        public bool Show
        {
            get => _show;
            set => SetProperty(ref _show, value);
        }
        public Finance Finance
        {
            get => _finance;
            set => SetProperty(ref _finance, value);
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public EditViewModel(IFinanceDataAccess<Finance> database)
        {
            _database = database;
            Show = false;
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CloseCommand = new RelayCommand(ExecuteClose);
        }

        private void ExecuteSave(object parameter)
        {
            _database.Modify(_finance);
        }

        private bool CanExecuteSave(object parameter)
        {
            // 실행 가능한지 여부를 결정하는 로직을 구현합니다.
            return true;
        }

        private void ExecuteClose(object parameter)
        {
            Show = false;
        }
    }
}