using HomeAccount.Commands;
using HomeAccount.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeAccount.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        public event Action OnCategoryChanged;

        private bool _show;
        private bool _showEdit;
        private string _header;
        private string _name;
        private string _editedName;
        private CategoryBase _selectedCategory;
        private ICategoryContext _categoryContext;

        public bool Show
        {
            get => _show;
            set => SetProperty(ref _show, value);
        }
        public bool ShowEdit
        {
            get => _showEdit;
            set => SetProperty(ref _showEdit, value);
        }
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string EditedName
        {
            get => _editedName;
            set => SetProperty(ref _editedName, value);
        }
        public CategoryBase SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }
        public ObservableCollection<CategoryBase> Categories { get; set; }
        public ICategoryContext CategoryContext
        {
            get => _categoryContext;
            set => SetCategoryContext(value);
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand ShowEditCommand { get; private set; }
        public ICommand CancelEditCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand ApplyCommand { get; private set; }

        public CategoryViewModel()
        {
            Show = false;
            ShowEdit = false;
            Categories = new ObservableCollection<CategoryBase>();
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            EditCommand = new RelayCommand(ExecuteEdit, CanExecuteEdit);
            RemoveCommand = new RelayCommand(ExecuteRemove, CanExecuteRemove);
            ShowEditCommand = new RelayCommand(ExecuteShowEdit, CanExecuteShowEdit);
            CancelEditCommand = new RelayCommand(ExecuteCancelEdit);
            CloseCommand = new RelayCommand(ExecuteClose);
            ApplyCommand = new RelayCommand(ExecuteApply);
        }

        private void SetCategoryContext(ICategoryContext value)
        {
            _categoryContext = value;
            Categories?.Clear();
            IEnumerable categories = _categoryContext.ReadAll();

            foreach (CategoryBase category in categories)
            {
                Categories.Add(category);
            }
        }

        private void ExecuteSave(object parameter)
        {
            var response = CategoryContext.Save(_name);

            if (response is CategoryBase category)
            {
                Categories.Add(category);
                OnCategoryChanged?.Invoke();
                MessageBox.Show($"'{category.Name}' 항목이 추가되었습니다.");
                Name = string.Empty;
            }
        }

        private bool CanExecuteSave(object parameter)
        {
            return !string.IsNullOrEmpty(_name);
        }

        private void ExecuteEdit(object parameter)
        {
            _selectedCategory.Name = _editedName;
            int response = CategoryContext.Edit(_selectedCategory);

            if (1 <= response)
            {
                Categories?.Clear();
                IEnumerable categories = _categoryContext.ReadAll();

                foreach (CategoryBase category in categories)
                {
                    Categories.Add(category);
                }

                SelectedCategory = Categories.FirstOrDefault(category => category.Id == response);
                OnCategoryChanged?.Invoke();
                MessageBox.Show($"'{_selectedCategory.Name}' 항목이 '{_editedName}'으로 변경되었습니다.");
                Name = string.Empty;
                EditedName = string.Empty;
                ShowEdit = false;
            }
        }

        private bool CanExecuteEdit(object parameter)
        {
            return !string.Equals(_name, _editedName);
        }

        private void ExecuteRemove(object parameter)
        {
            int response = CategoryContext.Remove(_selectedCategory);

            if (1 <= response)
            {
                OnCategoryChanged?.Invoke();
                MessageBox.Show($"'{_selectedCategory.Name}' 항목이 삭제되었습니다.");
                Categories.Remove(_selectedCategory);

                if (Categories[response] != null)
                {
                    SelectedCategory = Categories[response];
                }

                Name = string.Empty;
            }
        }

        private bool CanExecuteRemove(object parameter)
        {
            return _selectedCategory != null;
        }

        private void ExecuteClose(object parameter)
        {
            Show = false;
        }

        private void ExecuteShowEdit(object parameter)
        {
            if (_selectedCategory == null)
            {
                return;
            }

            EditedName = _selectedCategory.Name;
            ShowEdit = true;
        }

        private bool CanExecuteShowEdit(object parameter)
        {
            return _selectedCategory != null;
        }

        private void ExecuteCancelEdit(object parameter)
        {
            ShowEdit = false;
        }

        private void ExecuteApply(object parameter)
        {

        }
    }
}