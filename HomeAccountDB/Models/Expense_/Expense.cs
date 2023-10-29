using Dapper.Contrib.Extensions;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Models
{
    [Table("expense")]
    public class Expense
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public int SourceId { get; set; }
        public int MethodId { get; set; }
        public string Note { get; set; }
        public int IsDeleted { get; set; }
        public string CreatedAt { get; set; }

        public Expense()
        {

        }

        public Expense(ExpenseSaveRequest expenseSaveRequest)
        {
            Date = expenseSaveRequest.Date.ToString("yyyy/MM/dd");
            Amount = expenseSaveRequest.Amount;
            CategoryId = expenseSaveRequest.CategoryId;
            SourceId = expenseSaveRequest.SourceId;
            MethodId = expenseSaveRequest.MethodId;
            Note = expenseSaveRequest.Note;
            IsDeleted = 0;
            CreatedAt = DateTime.Today.ToString("yyyy/MM/dd");
        }

        public void Update(ExpenseUpdateRequest expenseUpdateRequest)
        {
            Date = expenseUpdateRequest.Date.ToString("yyyy/MM/dd");
            Amount = expenseUpdateRequest.Amount;
            CategoryId = expenseUpdateRequest.CategoryId;
            SourceId = expenseUpdateRequest.SourceId;
            MethodId = expenseUpdateRequest.MethodId;
            Note = expenseUpdateRequest.Note;
        }
    }
}