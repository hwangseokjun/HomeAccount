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
    public class IncomeSourceRepository : IIncomeSourceRepository
    {
        public bool Delete(IncomeSource entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Delete(entity);
            }
        }

        public IEnumerable<IncomeSource> GetAll()
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                IEnumerable<IncomeSource> incomeSources = connection.GetAll<IncomeSource>();

                return incomeSources;
            }
        }

        public IncomeSource GetById(int id)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Get<IncomeSource>(id);
            }
        }

        public int Insert(IncomeSource entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                long id = connection.Insert(entity);

                return (int)id;
            }
        }

        public bool Update(IncomeSource entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Update<IncomeSource>(entity);
            }
        }
    }
}
