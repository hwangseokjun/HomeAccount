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
    public class IncomeCategoryRepository : IIncomeCategoryRepository
    {
        public bool Delete(IncomeCategory entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Delete(entity);
            }
        }

        public IEnumerable<IncomeCategory> GetAll()
        {
            using(var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                IEnumerable<IncomeCategory> incomeCategories = connection.GetAll<IncomeCategory>();

                return incomeCategories;
            }
        }

        public IncomeCategory GetById(int id)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Get<IncomeCategory>(id);
            }
        }

        public int Insert(IncomeCategory entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                long id = connection.Insert(entity);

                return (int)id;
            }
        }

        public bool Update(IncomeCategory entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Update<IncomeCategory>(entity);
            }
        }
    }
}
