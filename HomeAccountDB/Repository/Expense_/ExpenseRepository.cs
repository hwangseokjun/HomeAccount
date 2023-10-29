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
    public class ExpenseRepository : IExpenseRepository
    {
        public bool Delete(Expense entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Delete(entity);
            }
        }

        public IEnumerable<Expense> GetAll()
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                IEnumerable<Expense> expenses = connection.GetAll<Expense>();

                return expenses;
            }
        }

        public IEnumerable<Expense> GetBetweenDate(string startDate, string endDate)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                string query = "SELECT * FROM expense WHERE Date BETWEEN @StartDate AND @EndDate;";
                IEnumerable<Expense> expenses = connection.Query<Expense>(query, (StartDate: startDate, EndDate: endDate));

                return expenses;
            }
        }

        public Expense GetById(int id)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Get<Expense>(id);
            }
        }

        public int Insert(Expense entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                long id = connection.Insert(entity);

                return (int)id;
            }
        }

        public bool Update(Expense entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Update<Expense>(entity);
            }
        }
    }
}