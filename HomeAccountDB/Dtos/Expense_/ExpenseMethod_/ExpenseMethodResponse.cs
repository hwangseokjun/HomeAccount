using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct ExpenseMethodResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public ExpenseMethodResponse(ExpenseMethod expenseMethod)
        {
            Id = expenseMethod.Id;
            Name = expenseMethod.Name;
            Sequence = expenseMethod.Sequence;
            IsDeleted = expenseMethod.IsDeleted;
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Sequence}, {IsDeleted}";
        }
    }
}