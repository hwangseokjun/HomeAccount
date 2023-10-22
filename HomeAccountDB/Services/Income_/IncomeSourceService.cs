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
    public class IncomeSourceService
    {
        private readonly IIncomeSourceRepository _incomeSourceRepository;

        public IncomeSourceService(IIncomeSourceRepository incomeSourceRepository)
        {
            _incomeSourceRepository = incomeSourceRepository;
        }

        public IEnumerable<IncomeSourceResponse> GetAll()
        {
            var incomeSourceResponses = new List<IncomeSourceResponse>();
            IEnumerable<IncomeSource> incomeSources = _incomeSourceRepository.GetAll();

            foreach (var incomeSource in incomeSources)
            {
                var incomeSourceResponse = new IncomeSourceResponse(incomeSource);
                incomeSourceResponses.Add(incomeSourceResponse);
            }

            return incomeSourceResponses;
        }

        public IncomeSourceResponse Save(IncomeSourceSaveRequest incomeSourceSaveRequest)
        {
            var incomeSource = new IncomeSource(incomeSourceSaveRequest);

            incomeSource.Id = _incomeSourceRepository.Insert(incomeSource);
            var incomeResponse = new IncomeSourceResponse(incomeSource);

            return incomeResponse;
        }

        public int Remove(int id)
        {
            IncomeSource source = _incomeSourceRepository.GetById(id);

            if (source == null)
            {
                return -1;
            }

            bool success = _incomeSourceRepository.Delete(source);

            return success ? id : -1;
        }

        public int Update(IncomeSourceUpdateRequest incomeSourceUpdateRequest)
        {
            IncomeSource source = _incomeSourceRepository.GetById(incomeSourceUpdateRequest.Id);

            if (source != null)
            {
                source.Update(incomeSourceUpdateRequest);
                bool success = _incomeSourceRepository.Update(source);

                return success ? incomeSourceUpdateRequest.Id : -1;
            }

            return -1;
        }
    }
}
