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
    public class FinanceDB : IFinanceDataAccess<Finance>
    {
        private readonly ExpenseController _expenseController;
        private readonly IncomeController _incomeController;

        public FinanceDB()
        {
            _expenseController = DatabaseSingleton.Instance.Database.Expense;
            _incomeController = DatabaseSingleton.Instance.Database.Income;
        }

        public Finance Add(Finance data)
        {
            if (data.Category is ExpenseCategory
                && data.Method is ExpenseMethod
                && data.Source is ExpenseSource)
            {
                var request = new ExpenseSaveRequest
                {
                    Date = data.Date,
                    Amount = data.Amount,
                    CategoryId = data.Category.Id,
                    SourceId = data.Source.Id,
                    MethodId = data.Method.Id,
                    Note = data.Note
                };
                ExpenseResponse response = _expenseController.Add(request);
                data.Id = response.Id;

                return data;
            }
            else 
            {
                var request = new IncomeSaveRequest
                {
                    Date = data.Date,
                    Amount = data.Amount,
                    CategoryId = data.Category.Id,
                    SourceId = data.Source.Id,
                    MethodId = data.Method.Id,
                    Note = data.Note
                };
                IncomeResponse response = _incomeController.Add(request);
                data.Id = response.Id;

                return data;
            }
        }

        public int Modify(Finance data)
        {
            if (data.Category is ExpenseCategory
                && data.Method is ExpenseMethod
                && data.Source is ExpenseSource)
            {
                var request = new ExpenseUpdateRequest
                {
                    Id = data.Id,
                    Date = data.Date,
                    Amount = data.Amount,
                    CategoryId = data.Category.Id,
                    SourceId = data.Source.Id,
                    MethodId = data.Method.Id,
                    Note = data.Note
                };
                int response = _expenseController.Modify(request);

                return response;
            }
            else
            {
                var request = new IncomeUpdateRequest
                {
                    Id = data.Id,
                    Date = data.Date,
                    Amount = data.Amount,
                    CategoryId = data.Category.Id,
                    SourceId = data.Source.Id,
                    MethodId = data.Method.Id,
                    Note = data.Note
                };
                int response = _incomeController.Modify(request);

                return response;
            }
        }

        public IEnumerable<Finance> ReadAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Finance> ReadBy()
        {
            throw new NotImplementedException();
        }

        public int Remove(Finance data)
        {
            if (data.Category is ExpenseCategory
                && data.Method is ExpenseMethod
                && data.Source is ExpenseSource)
            {
                return _expenseController.Delete(data.Id);
            }
            else
            {
                return _incomeController.Delete(data.Id);
            }
        }
    }
}