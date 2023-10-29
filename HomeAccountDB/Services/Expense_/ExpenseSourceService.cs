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
    public class ExpenseSourceService
    {
        private readonly IExpenseSourceRepository _expenseSourceRepository;

        public ExpenseSourceService(IExpenseSourceRepository expenseSourceRepository)
        {
            _expenseSourceRepository = expenseSourceRepository;
        }

        public IEnumerable<ExpenseSourceResponse> GetAll()
        {
            var expenseSourceResponses = new List<ExpenseSourceResponse>();
            IEnumerable<ExpenseSource> expenseSources = _expenseSourceRepository.GetAll();

            foreach (var expenseSource in expenseSources)
            {
                var expenseSourceResponse = new ExpenseSourceResponse(expenseSource);
                expenseSourceResponses.Add(expenseSourceResponse);
            }

            return expenseSourceResponses;
        }

        public ExpenseSourceResponse Save(ExpenseSourceSaveRequest expenseSourceSaveRequest)
        {
            var expenseSource = new ExpenseSource(expenseSourceSaveRequest);

            expenseSource.Id = _expenseSourceRepository.Insert(expenseSource);
            var expenseResponse = new ExpenseSourceResponse(expenseSource);

            return expenseResponse;
        }

        public int Remove(int id)
        {
            ExpenseSource source = _expenseSourceRepository.GetById(id);

            if (source == null)
            {
                return -1;
            }

            bool success = _expenseSourceRepository.Delete(source);

            return success ? id : -1;
        }

        public int Update(ExpenseSourceUpdateRequest expenseSourceUpdateRequest)
        {
            ExpenseSource source = _expenseSourceRepository.GetById(expenseSourceUpdateRequest.Id);

            if (source != null)
            {
                source.Update(expenseSourceUpdateRequest);
                bool success = _expenseSourceRepository.Update(source);

                return success ? expenseSourceUpdateRequest.Id : -1;
            }

            return -1;
        }
    }
}
