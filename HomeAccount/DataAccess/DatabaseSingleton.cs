using HomeAccountDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccount.DataAccess
{
    internal class DatabaseSingleton
    {
        private static Lazy<DatabaseSingleton> _instance = new Lazy<DatabaseSingleton>(() => new DatabaseSingleton());

        public static DatabaseSingleton Instance => _instance.Value;

        private DatabaseSingleton()
        {
            Database = new Database();
        }

        public Database Database { get; }
    }
}
