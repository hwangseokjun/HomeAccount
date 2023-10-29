using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct ExpenseResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public int SourceId { get; set; }
        public int MethodId { get; set; }
        public string Note { get; set; }

        public ExpenseResponse(Expense expense)
        {
            Id = expense.Id;
            Date = DateTime.Parse(expense.Date);
            Amount = expense.Amount;
            CategoryId = expense.CategoryId;
            SourceId = expense.SourceId;
            MethodId = expense.MethodId;
            Note = expense.Note;
        }
    }
}