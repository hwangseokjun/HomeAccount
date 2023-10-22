using HomeAccountDB.Dtos;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Controllers
{
    public class ExpenseController
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public ExpenseResponse Add(ExpenseSaveRequest incomeSaveRequest)
        {
            return _expenseService.Save(incomeSaveRequest);
        }

        public int Delete(int id)
        {
            return _expenseService.Remove(id);
        }

        public int Modify(ExpenseUpdateRequest incomeUpdateRequest)
        {
            return _expenseService.Update(incomeUpdateRequest);
        }
    }
}
