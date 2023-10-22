using Dapper.Contrib.Extensions;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Models
{
    [Table("expense_source")]
    public class ExpenseSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public ExpenseSource()
        {

        }

        public ExpenseSource(ExpenseSourceSaveRequest expenseSourceSaveRequest)
        {
            Name = expenseSourceSaveRequest.Name;
            Sequence = expenseSourceSaveRequest.Sequence;
        }

        public void Update(ExpenseSourceUpdateRequest expenseSourceUpdateRequest)
        {
            Name = expenseSourceUpdateRequest.Name;
            Sequence = expenseSourceUpdateRequest.Sequence;
        }
    }
}