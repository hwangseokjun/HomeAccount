﻿using HomeAccountDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Dtos
{
    public struct IncomeMethodResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsDeleted { get; set; }

        public IncomeMethodResponse(IncomeMethod incomeMethod)
        {
            Id = incomeMethod.Id;
            Name = incomeMethod.Name;
            Sequence = incomeMethod.Sequence;
            IsDeleted = incomeMethod.IsDeleted.ToBool();
        }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Sequence}, {IsDeleted}";
        }
    }
}
