using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccount.DataAccess
{
    public interface IFinanceDataAccess<T> : IDataAccess<T>
    {
        IEnumerable<T> ReadBy();
    }
}
