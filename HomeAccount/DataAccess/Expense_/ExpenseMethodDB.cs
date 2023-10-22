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
    public class ExpenseMethodDB : IDataAccess<ExpenseMethod>
    {
        private readonly ExpenseMethodController _expenseMethodController;

        public ExpenseMethodDB()
        {
            _expenseMethodController = DatabaseSingleton.Instance.Database.ExpenseMethod;
        }

        public ExpenseMethod Add(ExpenseMethod data)
        {
            var request = new ExpenseMethodSaveRequest
            {
                Name = data.Name,
                Sequence = data.Sequence
            };
            ExpenseMethodResponse response = _expenseMethodController.Add(request);

            return new ExpenseMethod
            {
                Id = response.Id,
                Name = response.Name,
                Sequence = response.Sequence
            };
        }

        public int Modify(ExpenseMethod data)
        {
            var request = new ExpenseMethodUpdateRequest
            {
                Id = data.Id,
                Name = data.Name,
                Sequence = data.Sequence
            };
            int response = _expenseMethodController.Modify(request);

            return response;
        }

        public IEnumerable<ExpenseMethod> ReadAll()
        {
            IEnumerable<ExpenseMethodResponse> expenseMethodResponses = _expenseMethodController.ReadAll();
            int count = expenseMethodResponses.Count();
            var expenseMethods = new List<ExpenseMethod>(count);

            foreach (var response in expenseMethodResponses)
            {
                var category = new ExpenseMethod
                {
                    Id = response.Id,
                    Name = response.Name,
                    Sequence = response.Sequence
                };
                expenseMethods.Add(category);
            }

            return expenseMethods;
        }

        public int Remove(ExpenseMethod data)
        {
            return _expenseMethodController.Delete(data.Id);
        }
    }
}
