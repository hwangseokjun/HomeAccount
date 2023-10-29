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
    public class IncomeCategoryDB : IDataAccess<IncomeCategory>
    {
        private readonly IncomeCategoryController _incomeCategoryController;

        public IncomeCategoryDB()
        {
            _incomeCategoryController = DatabaseSingleton.Instance.Database.IncomeCategory;
        }

        public IncomeCategory Add(IncomeCategory data)
        {
            var request = new IncomeCategorySaveRequest
            {
                Name = data.Name,
                Sequence = data.Sequence
            };
            IncomeCategoryResponse response = _incomeCategoryController.Add(request);

            return new IncomeCategory
            {
                Id = response.Id,
                Name = response.Name,
                Sequence = response.Sequence
            };
        }

        public int Modify(IncomeCategory data)
        {
            var request = new IncomeCategoryUpdateRequest
            {
                Id = data.Id,
                Name = data.Name,
                Sequence = data.Sequence
            };
            int response = _incomeCategoryController.Modify(request);

            return response;
        }

        public IEnumerable<IncomeCategory> ReadAll()
        {
            IEnumerable<IncomeCategoryResponse> incomeCategoryResponses = _incomeCategoryController.ReadAll();
            int count = incomeCategoryResponses.Count();
            var incomeCategories = new List<IncomeCategory>(count);

            foreach (IncomeCategoryResponse response in incomeCategoryResponses)
            {
                var category = new IncomeCategory
                {
                    Id = response.Id,
                    Name = response.Name,
                    Sequence = response.Sequence
                };
                incomeCategories.Add(category);
            }

            return incomeCategories;
        }

        public int Remove(IncomeCategory data)
        {
            return _incomeCategoryController.Delete(data.Id);
        }
    }
}