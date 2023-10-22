using HomeAccountDB.Dtos;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Controllers
{
    public class ExpenseMethodController
    {
        private readonly ExpenseMethodService _expenseMethodService;

        public ExpenseMethodController(ExpenseMethodService expenseMethodService)
        {
            _expenseMethodService = expenseMethodService;
        }

        public IEnumerable<ExpenseMethodResponse> ReadAll()
        {
            return _expenseMethodService.GetAll();
        }

        public ExpenseMethodResponse Add(ExpenseMethodSaveRequest incomeMethodSaveRequest)
        {
            return _expenseMethodService.Save(incomeMethodSaveRequest);
        }

        public int Delete(int id)
        {
            return _expenseMethodService.Remove(id);
        }

        public int Modify(ExpenseMethodUpdateRequest incomeMethodUpdateRequest)
        {
            return _expenseMethodService.Update(incomeMethodUpdateRequest);
        }
    }
}
