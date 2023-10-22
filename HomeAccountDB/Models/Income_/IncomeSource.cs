using Dapper.Contrib.Extensions;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Models
{
    [Table("income_source")]
    public class IncomeSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public IncomeSource()
        {

        }

        public IncomeSource(IncomeSourceSaveRequest incomeSourceSaveRequest)
        {
            Name = incomeSourceSaveRequest.Name;
            Sequence = incomeSourceSaveRequest.Sequence;
        }

        public void Update(IncomeSourceUpdateRequest incomeSourceUpdateRequest) 
        {
            Name = incomeSourceUpdateRequest.Name;
            Sequence = incomeSourceUpdateRequest.Sequence;
        }
    }
}