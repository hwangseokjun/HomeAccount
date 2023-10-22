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
    public class IncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public IncomeResponse Save(IncomeSaveRequest incomeSaveRequest)
        {
            var income = new Income(incomeSaveRequest);
            income.Id = _incomeRepository.Insert(income);
            var incomeResponse = new IncomeResponse(income);

            return incomeResponse;
        }

        public int Remove(int id)
        {
            Income income = _incomeRepository.GetById(id);

            if (income == null)
            {
                return -1;
            }

            bool success = _incomeRepository.Delete(income);

            return success ? id : -1;
        }

        public int Update(IncomeUpdateRequest incomeUpdateRequest)
        {
            Income income = _incomeRepository.GetById(incomeUpdateRequest.Id);

            if (income != null)
            {
                income.Update(incomeUpdateRequest);
                bool success = _incomeRepository.Update(income);

                return success ? incomeUpdateRequest.Id : -1;
            }

            return -1;
        }

        public IEnumerable<IncomeResponse> GetBeetween(DateTime startDate, DateTime endDate)
        {
            var incomeResponses = new List<IncomeResponse>();
            string startDateString = startDate.ToString("yyyy/MM/dd");
            string endDateString = endDate.ToString("yyyy/MM/dd");
            IEnumerable<Income> incomes = _incomeRepository.GetBetweenDate(startDateString, endDateString);

            foreach (var income in incomes)
            {
                if (income.IsDeleted)
                {
                    continue;
                }

                var incomeResponse = new IncomeResponse(income);
                incomeResponses.Add(incomeResponse);
            }

            return incomeResponses;
        }
    }
}