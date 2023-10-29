using Dapper.Contrib.Extensions;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Models
{
    [Table("income_category")]
    public class IncomeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public int IsDeleted { get; set; }

        public IncomeCategory()
        {

        }

        public IncomeCategory(IncomeCategorySaveRequest incomeCategorySaveRequest)
        {
            Name = incomeCategorySaveRequest.Name;
            Sequence = incomeCategorySaveRequest.Sequence;
            IsDeleted = 0;
        }

        public void Update(IncomeCategoryUpdateRequest incomeCategoryUpdateRequest)
        {
            Name = incomeCategoryUpdateRequest.Name;
            Sequence = incomeCategoryUpdateRequest.Sequence;
        }
    }
}