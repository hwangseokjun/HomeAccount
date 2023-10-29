using HomeAccountDB.Dtos;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Controllers
{
    public class ExpenseCategoryController
    {
        private readonly ExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoryController(ExpenseCategoryService expenseCategoryService)
        {
            _expenseCategoryService = expenseCategoryService;
        }

        public IEnumerable<ExpenseCategoryResponse> ReadAll()
        {
            return _expenseCategoryService.GetAll();
        }

        public ExpenseCategoryResponse Add(ExpenseCategorySaveRequest incomeCategorySaveRequest)
        {
            return _expenseCategoryService.Save(incomeCategorySaveRequest);
        }

        public int Delete(int id)
        {
            return _expenseCategoryService.Remove(id);
        }

        public int Modify(ExpenseCategoryUpdateRequest incomeCategoryUpdateRequest)
        {
            return _expenseCategoryService.Update(incomeCategoryUpdateRequest);
        }
    }
}
