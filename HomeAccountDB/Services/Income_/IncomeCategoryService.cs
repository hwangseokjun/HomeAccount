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
    public class IncomeCategoryService
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        public IncomeCategoryService(IIncomeCategoryRepository incomeCategoryRepository)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        public IEnumerable<IncomeCategoryResponse> GetAll()
        {
            var incomeCategoryResponses = new List<IncomeCategoryResponse>();
            IEnumerable<IncomeCategory> incomeCategories = _incomeCategoryRepository.GetAll();

            foreach (var incomeCategory in incomeCategories)
            {
                var incomeCategoryResponse = new IncomeCategoryResponse(incomeCategory);
                incomeCategoryResponses.Add(incomeCategoryResponse);
            }

            return incomeCategoryResponses;
        }

        public IncomeCategoryResponse Save(IncomeCategorySaveRequest incomeCategorySaveRequest)
        {
            var incomeCategory = new IncomeCategory(incomeCategorySaveRequest);

            incomeCategory.Id = _incomeCategoryRepository.Insert(incomeCategory);
            var incomeResponse = new IncomeCategoryResponse(incomeCategory);

            return incomeResponse;
        }

        public int Remove(int id)
        {
            IncomeCategory category = _incomeCategoryRepository.GetById(id);

            if (category == null)
            {
                return -1;
            }

            bool success = _incomeCategoryRepository.Delete(category);

            return success ? id : -1;
        }

        public int Update(IncomeCategoryUpdateRequest incomeCategoryUpdateRequest)
        {
            IncomeCategory category = _incomeCategoryRepository.GetById(incomeCategoryUpdateRequest.Id);

            if (category != null)
            {
                category.Update(incomeCategoryUpdateRequest);
                bool success = _incomeCategoryRepository.Update(category);

                return success ? incomeCategoryUpdateRequest.Id : -1;
            }

            return -1;
        }
    }
}