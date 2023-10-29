using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Repository
{
    public interface IRepository<T, ID> where T : class
    {
        ID Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IEnumerable<T> GetAll();
        T GetById(ID id);
    }
}
