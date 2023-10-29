using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct ExpenseSourceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public ExpenseSourceResponse(ExpenseSource expenseSource)
        {
            Id = expenseSource.Id;
            Name = expenseSource.Name;
            Sequence = expenseSource.Sequence;
            IsDeleted = expenseSource.IsDeleted.ToBool();
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Sequence}, {IsDeleted}";
        }
    }
}
