using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct IncomeCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public IncomeCategoryResponse(IncomeCategory incomeCategory)
        {
            Id = incomeCategory.Id;
            Name = incomeCategory.Name;
            Sequence = incomeCategory.Sequence;
            IsDeleted = incomeCategory.IsDeleted.ToBool();
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Sequence}, {IsDeleted}";
        }
    }
}