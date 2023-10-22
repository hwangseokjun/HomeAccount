using Dapper.Contrib.Extensions;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Models
{
    [Table("expense_category")]
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public ExpenseCategory()
        {

        }

        public ExpenseCategory(ExpenseCategorySaveRequest expenseCategorySaveRequest)
        {
            Name = expenseCategorySaveRequest.Name;
            Sequence = expenseCategorySaveRequest.Sequence;
        }

        public void Update(ExpenseCategoryUpdateRequest expenseCategoryUpdateRequest)
        {
            Name = expenseCategoryUpdateRequest.Name;
            Sequence = expenseCategoryUpdateRequest.Sequence;
        }
    }
}