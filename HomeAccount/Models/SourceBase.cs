using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccount.Models
{
    public abstract class SourceBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}