using Dapper;
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
    public class ExpenseSourceRepository : IExpenseSourceRepository
    {
        public bool Delete(ExpenseSource entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpenseSource> GetAll()
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                IEnumerable<ExpenseSource> expenseSources = connection.GetAll<ExpenseSource>();

                return expenseSources;
            }
        }

        public ExpenseSource GetById(int id)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                ExpenseSource expenseSource = connection.Get<ExpenseSource>(id);

                return expenseSource;
            }
        }

        public int Insert(ExpenseSource entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                long id = connection.Insert(entity);

                return (int)id;
            }
        }

        public bool Update(ExpenseSource entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Update<ExpenseSource>(entity);
            }
        }
    }
}