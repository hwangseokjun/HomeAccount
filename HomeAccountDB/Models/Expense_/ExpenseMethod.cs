using Dapper.Contrib.Extensions;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Models
{
    [Table("expense_method")]
    public class ExpenseMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public ExpenseMethod()
        {

        }

        public ExpenseMethod(ExpenseMethodSaveRequest expenseMethodSaveRequest)
        {
            Name = expenseMethodSaveRequest.Name;
            Sequence = expenseMethodSaveRequest.Sequence;
        }

        public void Update(ExpenseMethodUpdateRequest expenseMethodUpdateRequest)
        {
            Name = expenseMethodUpdateRequest.Name;
            Sequence = expenseMethodUpdateRequest.Sequence;
        }
    }
}