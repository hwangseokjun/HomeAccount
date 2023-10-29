using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct IncomeResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public int SourceId { get; set; }
        public string Note { get; set; }

        public IncomeResponse(Income income)
        {
            Id = income.Id;
            Date = DateTime.Parse(income.Date);
            Amount = income.Amount;
            CategoryId = income.CategoryId;
            SourceId = income.SourceId;
            Note = income.Note;
        }
    }
}