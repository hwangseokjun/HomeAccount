using HomeAccount.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccount.Models
{
    public class Finance
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public CategoryBase Category { get; set; }
        public SourceBase Source { get; set; }
        public MethodBase Method { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Amount}, {Date}, {Note}, {Category}, {Source}, {Method}";
        }
    }
}