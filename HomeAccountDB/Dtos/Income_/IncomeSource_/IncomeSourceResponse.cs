using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct IncomeSourceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public IncomeSourceResponse(IncomeSource incomeSource)
        {
            Id = incomeSource.Id;
            Name = incomeSource.Name;
            Sequence = incomeSource.Sequence;
            IsDeleted = incomeSource.IsDeleted;
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Sequence}, {IsDeleted}";
        }
    }
}
