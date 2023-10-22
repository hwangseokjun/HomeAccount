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
    public class ExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public ExpenseResponse Save(ExpenseSaveRequest expenseSaveRequest)
        {
            var expense = new Expense(expenseSaveRequest);
            expense.Id = _expenseRepository.Insert(expense);
            var expenseResponse = new ExpenseResponse(expense);

            return expenseResponse;
        }

        public int Remove(int id)
        {
            Expense expense = _expenseRepository.GetById(id);

            if (expense == null)
            {
                return -1;
            }

            bool success = _expenseRepository.Delete(expense);

            return success ? id : -1;
        }

        public int Update(ExpenseUpdateRequest expenseUpdateRequest)
        {
            Expense expense = _expenseRepository.GetById(expenseUpdateRequest.Id);

            if (expense != null)
            {
                expense.Update(expenseUpdateRequest);
                bool success = _expenseRepository.Update(expense);

                return success ? expenseUpdateRequest.Id : -1;
            }

            return -1;
        }

        public IEnumerable<ExpenseResponse> GetBeetween(DateTime startDate, DateTime endDate)
        {
            var expenseResponses = new List<ExpenseResponse>();
            string startDateString = startDate.ToString("yyyy/MM/dd");
            string endDateString = endDate.ToString("yyyy/MM/dd");
            IEnumerable<Expense> expenses = _expenseRepository.GetBetweenDate(startDateString, endDateString);

            foreach (var expense in expenses)
            {
                if (expense.IsDeleted)
                {
                    continue;
                }

                var expenseResponse = new ExpenseResponse(expense);
                expenseResponses.Add(expenseResponse);
            }

            return expenseResponses;
        }
    }
}
