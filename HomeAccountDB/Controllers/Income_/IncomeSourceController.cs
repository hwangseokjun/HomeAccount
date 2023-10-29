using HomeAccountDB.Dtos;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Controllers
{
    public class IncomeSourceController
    {
        private readonly IncomeSourceService _incomeSourceService;

        public IncomeSourceController(IncomeSourceService incomeSourceService)
        {
            _incomeSourceService = incomeSourceService;
        }

        public IEnumerable<IncomeSourceResponse> ReadAll()
        {
            return _incomeSourceService.GetAll();
        }

        public IncomeSourceResponse Add(IncomeSourceSaveRequest incomeSourceSaveRequest)
        {
            return _incomeSourceService.Save(incomeSourceSaveRequest);
        }

        public int Delete(int id)
        {
            return _incomeSourceService.Remove(id);
        }

        public int Modify(IncomeSourceUpdateRequest incomeSourceUpdateRequest)
        {
            return _incomeSourceService.Update(incomeSourceUpdateRequest);
        }
    }
}
