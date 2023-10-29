using HomeAccountDB.Dtos;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Controllers
{
    public class ExpenseSourceController
    {
        private readonly ExpenseSourceService _expenseSourceService;

        public ExpenseSourceController(ExpenseSourceService expenseSourceService)
        {
            _expenseSourceService = expenseSourceService;
        }

        public IEnumerable<ExpenseSourceResponse> ReadAll()
        {
            return _expenseSourceService.GetAll();
        }

        public ExpenseSourceResponse Add(ExpenseSourceSaveRequest incomeSourceSaveRequest)
        {
            return _expenseSourceService.Save(incomeSourceSaveRequest);
        }

        public int Delete(int id)
        {
            return _expenseSourceService.Remove(id);
        }

        public int Modify(ExpenseSourceUpdateRequest incomeSourceUpdateRequest)
        {
            return _expenseSourceService.Update(incomeSourceUpdateRequest);
        }
    }
}