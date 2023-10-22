using HomeAccount.Models;
using HomeAccountDB.Controllers;
using HomeAccountDB.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccount.DataAccess
{
    public class IncomeMethodDB : IDataAccess<IncomeMethod>
    {
        private readonly IncomeMethodController _incomeMethodController;

        public IncomeMethodDB()
        {
            _incomeMethodController = DatabaseSingleton.Instance.Database.IncomeMethod;
        }

        public IncomeMethod Add(IncomeMethod data)
        {
            var request = new IncomeMethodSaveRequest
            {
                Name = data.Name,
                Sequence = data.Sequence
            };
            IncomeMethodResponse response = _incomeMethodController.Add(request);

            return new IncomeMethod
            {
                Id = response.Id,
                Name = response.Name,
                Sequence = response.Sequence
            };
        }

        public int Modify(IncomeMethod data)
        {
            var request = new IncomeMethodUpdateRequest
            {
                Id = data.Id,
                Name = data.Name,
                Sequence = data.Sequence
            };
            int response = _incomeMethodController.Modify(request);

            return response;
        }

        public IEnumerable<IncomeMethod> ReadAll()
        {
            IEnumerable<IncomeMethodResponse> incomeMethodResponses = _incomeMethodController.ReadAll();
            int count = incomeMethodResponses.Count();
            var incomeMethods = new List<IncomeMethod>(count);

            foreach (IncomeMethodResponse response in incomeMethodResponses)
            {
                var Method = new IncomeMethod
                {
                    Id = response.Id,
                    Name = response.Name,
                    Sequence = response.Sequence
                };
                incomeMethods.Add(Method);
            }

            return incomeMethods;
        }

        public int Remove(IncomeMethod data)
        {
            return _incomeMethodController.Delete(data.Id);
        }
    }
}
