using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct IncomeMethodUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
    }
}
