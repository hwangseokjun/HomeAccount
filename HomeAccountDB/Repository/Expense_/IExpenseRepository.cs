using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Repository
{
    public interface IExpenseRepository : IRepository<Expense, int>
    {
        IEnumerable<Expense> GetBetweenDate(string startDate, string endDate);
    }
}