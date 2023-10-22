using HomeAccountDB.Dtos;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Controllers
{
    public class IncomeMethodController
    {
        private readonly IncomeMethodService _incomeMethodService;

        public IncomeMethodController(IncomeMethodService incomeMethodService)
        {
            _incomeMethodService = incomeMethodService;
        }

        public IEnumerable<IncomeMethodResponse> ReadAll()
        {
            return _incomeMethodService.GetAll();
        }

        public IncomeMethodResponse Add(IncomeMethodSaveRequest incomeMethodSaveRequest)
        {
            return _incomeMethodService.Save(incomeMethodSaveRequest);
        }

        public int Delete(int id)
        {
            return _incomeMethodService.Remove(id);
        }

        public int Modify(IncomeMethodUpdateRequest incomeMethodUpdateRequest)
        {
            return _incomeMethodService.Update(incomeMethodUpdateRequest);
        }
    }
}
