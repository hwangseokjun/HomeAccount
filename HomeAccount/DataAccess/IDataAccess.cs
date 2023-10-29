using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccount.DataAccess
{
    public interface IDataAccess<T>
    {
        IEnumerable<T> ReadAll();
        T Add(T data);
        int Remove(T data);
        int Modify(T data);
    }
}