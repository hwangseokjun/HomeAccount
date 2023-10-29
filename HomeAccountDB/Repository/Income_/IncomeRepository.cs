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
    public class IncomeRepository : IIncomeRepository
    {
        public bool Delete(Income entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Delete(entity);
            }
        }

        public IEnumerable<Income> GetBetweenDate(string startDate, string endDate)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                string query = "SELECT * FROM income WHERE Date BETWEEN @StartDate AND @EndDate;";
                IEnumerable<Income> incomes = connection.Query<Income>(query, (StartDate: startDate, EndDate: endDate));

                return incomes;
            }
        }

        public IEnumerable<Income> GetAll()
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                IEnumerable<Income> incomes = connection.GetAll<Income>();

                return incomes;
            }
        }

        public Income GetById(int id)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Get<Income>(id);
            }
        }

        public int Insert(Income entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                long id = connection.Insert(entity);

                return (int)id;
            }
        }

        public bool Update(Income entity)
        {
            using (var connection = new SQLiteConnection(Settings.Default.CONN_STR))
            {
                return connection.Update<Income>(entity);
            }
        }
    }
}
