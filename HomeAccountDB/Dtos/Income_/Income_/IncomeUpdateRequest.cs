using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct IncomeUpdateRequest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public int SourceId { get; set; }
        public int MethodId { get; set; }
        public string Note { get; set; }
    }
}
