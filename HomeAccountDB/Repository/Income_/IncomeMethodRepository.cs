using Dapper.Contrib.Extensions;
using HomeAccountDB.Models;
using HomeAccountDB.Properties;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Repository
{
    public class IncomeMethodRepository : IIncomeMethodRepository
    {
        public bool Delete(IncomeMethod entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Delete(entity);
            }
        }

        public IEnumerable<IncomeMethod> GetAll()
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                IEnumerable<IncomeMethod> incomeMethods = connection.GetAll<IncomeMethod>();

                return incomeMethods;
            }
        }

        public IncomeMethod GetById(int id)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Get<IncomeMethod>(id);
            }
        }

        public int Insert(IncomeMethod entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                long id = connection.Insert(entity);

                return (int)id;
            }
        }

        public bool Update(IncomeMethod entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Update<IncomeMethod>(entity);
            }
        }
    }
}
