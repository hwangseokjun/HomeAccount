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
    public class IncomeMethodService
    {
        private readonly IIncomeMethodRepository _incomeMethodRepository;

        public IncomeMethodService(IIncomeMethodRepository incomeMethodRepository)
        {
            _incomeMethodRepository = incomeMethodRepository;
        }

        public IEnumerable<IncomeMethodResponse> GetAll()
        {
            var incomeMethodResponses = new List<IncomeMethodResponse>();
            IEnumerable<IncomeMethod> incomeMethods = _incomeMethodRepository.GetAll();

            foreach (var incomeMethod in incomeMethods)
            {
                var incomeMethodResponse = new IncomeMethodResponse(incomeMethod);
                incomeMethodResponses.Add(incomeMethodResponse);
            }

            return incomeMethodResponses;
        }

        public IncomeMethodResponse Save(IncomeMethodSaveRequest incomeMethodSaveRequest)
        {
            var incomeMethod = new IncomeMethod(incomeMethodSaveRequest);

            incomeMethod.Id = _incomeMethodRepository.Insert(incomeMethod);
            var incomeResponse = new IncomeMethodResponse(incomeMethod);

            return incomeResponse;
        }

        public int Remove(int id)
        {
            IncomeMethod method = _incomeMethodRepository.GetById(id);

            if (method == null)
            {
                return -1;
            }

            bool success = _incomeMethodRepository.Delete(method);

            return success ? id : -1;
        }

        public int Update(IncomeMethodUpdateRequest incomeMethodUpdateRequest)
        {
            IncomeMethod method = _incomeMethodRepository.GetById(incomeMethodUpdateRequest.Id);

            if (method != null)
            {
                method.Update(incomeMethodUpdateRequest);
                bool success = _incomeMethodRepository.Update(method);

                return success ? incomeMethodUpdateRequest.Id : -1;
            }

            return -1;
        }
    }
}