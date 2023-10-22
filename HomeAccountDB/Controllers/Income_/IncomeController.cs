using HomeAccountDB.Dtos;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Controllers
{
    public class IncomeController
    {
        private readonly IncomeService _incomeService;

        public IncomeController(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        public IEnumerable<IncomeResponse> GetBeetween(DateTime startDate, DateTime endDate)
        {
            return _incomeService.GetBeetween(startDate, endDate);
        }

        public IncomeResponse Add(IncomeSaveRequest incomeSaveRequest)
        {
            return _incomeService.Save(incomeSaveRequest);
        }

        public int Delete(int id)
        {
            return _incomeService.Remove(id);
        }

        public int Modify(IncomeUpdateRequest incomeUpdateRequest)
        {
            return _incomeService.Update(incomeUpdateRequest);
        }
    }
}