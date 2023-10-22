using HomeAccountDB.Dtos;
using HomeAccountDB.Models;
using HomeAccountDB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Services
{
    public class ExpenseCategoryService
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public ExpenseCategoryService(IExpenseCategoryRepository expenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public IEnumerable<ExpenseCategoryResponse> GetAll()
        {
            var expenseCategoryResponses = new List<ExpenseCategoryResponse>();
            IEnumerable<ExpenseCategory> expenseCategories = _expenseCategoryRepository.GetAll();

            foreach (var expenseCategory in expenseCategories)
            {
                var expenseCategoryResponse = new ExpenseCategoryResponse(expenseCategory);
                expenseCategoryResponses.Add(expenseCategoryResponse);
            }

            return expenseCategoryResponses;
        }

        public ExpenseCategoryResponse Save(ExpenseCategorySaveRequest expenseCategorySaveRequest)
        {
            var expenseCategory = new ExpenseCategory(expenseCategorySaveRequest);

            expenseCategory.Id = _expenseCategoryRepository.Insert(expenseCategory);
            var expenseResponse = new ExpenseCategoryResponse(expenseCategory);

            return expenseResponse;
        }

        public int Remove(int id)
        {
            ExpenseCategory category = _expenseCategoryRepository.GetById(id);

            if (category == null)
            {
                return -1;
            }

            bool success = _expenseCategoryRepository.Delete(category);

            return success ? id : -1;
        }

        public int Update(ExpenseCategoryUpdateRequest expenseCategoryUpdateRequest)
        {
            ExpenseCategory category = _expenseCategoryRepository.GetById(expenseCategoryUpdateRequest.Id);

            if (category != null)
            {
                category.Update(expenseCategoryUpdateRequest);
                bool success = _expenseCategoryRepository.Update(category);

                return success ? expenseCategoryUpdateRequest.Id : -1;
            }

            return -1;
        }
    }
}
