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
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        public bool Delete(ExpenseCategory entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Delete(entity);
            }
        }

        public IEnumerable<ExpenseCategory> GetAll()
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                IEnumerable<ExpenseCategory> expenseCategories = connection.GetAll<ExpenseCategory>();

                return expenseCategories;
            }
        }

        public ExpenseCategory GetById(int id)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Get<ExpenseCategory>(id);
            }
        }

        public int Insert(ExpenseCategory entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                long id = connection.Insert(entity);

                return (int)id;
            }
        }

        public bool Update(ExpenseCategory entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Update<ExpenseCategory>(entity);
            }
        }
    }
}