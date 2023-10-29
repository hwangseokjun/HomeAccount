using HomeAccount.Models;
using HomeAccountDB.Controllers;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccount.DataAccess
{
    public class ExpenseCategoryDB : IDataAccess<ExpenseCategory>
    {
        private readonly ExpenseCategoryController _expenseCategoryController;

        public ExpenseCategoryDB()
        {
            _expenseCategoryController = DatabaseSingleton.Instance.Database.ExpenseCategory;
        }

        public ExpenseCategory Add(ExpenseCategory data)
        {
            var request = new ExpenseCategorySaveRequest
            {
                Name = data.Name,
                Sequence = data.Sequence
            };
            ExpenseCategoryResponse response = _expenseCategoryController.Add(request);

            return new ExpenseCategory
            {
                Id = response.Id,
                Name = response.Name,
                Sequence = response.Sequence
            };
        }

        public int Modify(ExpenseCategory data)
        {
            var request = new ExpenseCategoryUpdateRequest
            {
                Id = data.Id,
                Name = data.Name,
                Sequence = data.Sequence
            };
            int response = _expenseCategoryController.Modify(request);

            return response;
        }

        public IEnumerable<ExpenseCategory> ReadAll()
        {
            IEnumerable<ExpenseCategoryResponse> expenseCategoryResponses = _expenseCategoryController.ReadAll();
            int count = expenseCategoryResponses.Count();
            var expenseCategories = new List<ExpenseCategory>(count);

            foreach (ExpenseCategoryResponse response in expenseCategoryResponses)
            {
                var category = new ExpenseCategory
                {
                    Id = response.Id,
                    Name = response.Name,
                    Sequence = response.Sequence
                };
                expenseCategories.Add(category);
            }

            return expenseCategories;
        }

        public int Remove(ExpenseCategory data)
        {
            return _expenseCategoryController.Delete(data.Id);
        }
    }
}
