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
    public class ExpenseSourceDB : IDataAccess<ExpenseSource>
    {
        private readonly ExpenseSourceController _expenseSourceController;

        public ExpenseSourceDB()
        {
            _expenseSourceController = DatabaseSingleton.Instance.Database.ExpenseSource;
        }

        public ExpenseSource Add(ExpenseSource data)
        {
            var request = new ExpenseSourceSaveRequest
            {
                Name = data.Name,
                Sequence = data.Sequence
            };
            ExpenseSourceResponse response = _expenseSourceController.Add(request);

            return new ExpenseSource
            {
                Id = response.Id,
                Name = response.Name,
                Sequence = response.Sequence
            };
        }

        public int Modify(ExpenseSource data)
        {
            var request = new ExpenseSourceUpdateRequest
            {
                Id = data.Id,
                Name = data.Name,
                Sequence = data.Sequence
            };
            int response = _expenseSourceController.Modify(request);

            return response;
        }

        public IEnumerable<ExpenseSource> ReadAll()
        {
            IEnumerable<ExpenseSourceResponse> expenseSourceResponses = _expenseSourceController.ReadAll();
            int count = expenseSourceResponses.Count();
            var expenseSources = new List<ExpenseSource>(count);

            foreach (var response in expenseSourceResponses)
            {
                var source = new ExpenseSource
                {
                    Id = response.Id,
                    Name = response.Name,
                    Sequence = response.Sequence
                };
                expenseSources.Add(source);
            }

            return expenseSources;
        }

        public int Remove(ExpenseSource data)
        {
            return _expenseSourceController.Delete(data.Id);
        }
    }
}
