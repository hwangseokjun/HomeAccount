using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Repository
{
    public interface IIncomeRepository : IRepository<Income, int>
    {
        IEnumerable<Income> GetBetweenDate(string startDate, string endDate);
    }
}