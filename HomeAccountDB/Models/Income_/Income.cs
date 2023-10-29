using Dapper.Contrib.Extensions;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Models
{
    [Table("income")]
    public class Income
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

        public Income()
        {

        }

        public Income(IncomeSaveRequest incomeSaveRequest)
        {
            Date = incomeSaveRequest.Date.ToString("yyyy/MM/dd");
            Amount = incomeSaveRequest.Amount;
            CategoryId = incomeSaveRequest.CategoryId;
            SourceId = incomeSaveRequest.SourceId;
            MethodId = incomeSaveRequest.MethodId;
            Note = incomeSaveRequest.Note;
            IsDeleted = 0;
            CreatedAt = DateTime.Today.ToString("yyyy/MM/dd");
        }

        public void Update(IncomeUpdateRequest incomeUpdateRequest)
        {
            Date = incomeUpdateRequest.Date.ToString("yyyy/MM/dd");
            Amount = incomeUpdateRequest.Amount;
            CategoryId = incomeUpdateRequest.CategoryId;
            SourceId = incomeUpdateRequest.SourceId;
            MethodId = incomeUpdateRequest.MethodId;
            Note = incomeUpdateRequest.Note;
        }
    }
}