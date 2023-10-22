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
    public class ExpenseMethodService
    {
        private readonly IExpenseMethodRepository _expenseMethodRepository;

        public ExpenseMethodService(IExpenseMethodRepository expenseMethodRepository)
        {
            _expenseMethodRepository = expenseMethodRepository;
        }

        public IEnumerable<ExpenseMethodResponse> GetAll()
        {
            var expenseMethodResponses = new List<ExpenseMethodResponse>();
            IEnumerable<ExpenseMethod> expenseMethods = _expenseMethodRepository.GetAll();

            foreach (var expenseMethod in expenseMethods)
            {
                var expenseMethodResponse = new ExpenseMethodResponse(expenseMethod);
                expenseMethodResponses.Add(expenseMethodResponse);
            }

            return expenseMethodResponses;
        }

        public ExpenseMethodResponse Save(ExpenseMethodSaveRequest expenseMethodSaveRequest)
        {
            var expenseMethod = new ExpenseMethod(expenseMethodSaveRequest);

            expenseMethod.Id = _expenseMethodRepository.Insert(expenseMethod);
            var expenseResponse = new ExpenseMethodResponse(expenseMethod);

            return expenseResponse;
        }

        public int Remove(int id)
        {
            ExpenseMethod category = _expenseMethodRepository.GetById(id);

            if (category == null)
            {
                return -1;
            }

            bool success = _expenseMethodRepository.Delete(category);

            return success ? id : -1;
        }

        public int Update(ExpenseMethodUpdateRequest expenseMethodUpdateRequest)
        {
            ExpenseMethod method = _expenseMethodRepository.GetById(expenseMethodUpdateRequest.Id);

            if (method != null)
            {
                method.Update(expenseMethodUpdateRequest);
                bool success = _expenseMethodRepository.Update(method);

                return success ? expenseMethodUpdateRequest.Id : -1;
            }

            return -1;
        }
    }
}
