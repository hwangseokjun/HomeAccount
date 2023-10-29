using Dapper.Contrib.Extensions;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Models
{
    [Table("income_method")]
    public class IncomeMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public int IsDeleted { get; set; }

        public IncomeMethod()
        {

        }

        public IncomeMethod(IncomeMethodSaveRequest incomeMethodSaveRequest)
        {
            Name = incomeMethodSaveRequest.Name;
            Sequence = incomeMethodSaveRequest.Sequence;
            IsDeleted = 0;
        }

        public void Update(IncomeMethodUpdateRequest incomeMethodUpdateRequest)
        {
            Name = incomeMethodUpdateRequest.Name;
            Sequence = incomeMethodUpdateRequest.Sequence;
        }
    }
}
