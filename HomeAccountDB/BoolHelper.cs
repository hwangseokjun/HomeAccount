using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB
{
    public static class BoolHelper
    {
        public static bool ToBool(this int value)
        {
            return value != 0;
        }

        public static int ToInt(this bool value)
        {
            return value ? 1 : 0;
        }
    }
}
