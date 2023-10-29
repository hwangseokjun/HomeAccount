using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct ExpenseCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public ExpenseCategoryResponse(ExpenseCategory expenseCategory)
        {
            Id = expenseCategory.Id;
            Name = expenseCategory.Name;
            Sequence = expenseCategory.Sequence;
            IsDeleted = expenseCategory.IsDeleted.ToBool();
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Sequence}, {IsDeleted}";
        }
    }
}