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
    public class IncomeSourceDB : IDataAccess<IncomeSource>
    {
        private readonly IncomeSourceController _incomeSourceController;

        public IncomeSourceDB()
        {
            _incomeSourceController = DatabaseSingleton.Instance.Database.IncomeSource;
        }

        public IncomeSource Add(IncomeSource data)
        {
            var request = new IncomeSourceSaveRequest
            {
                Name = data.Name,
                Sequence = data.Sequence
            };
            IncomeSourceResponse response = _incomeSourceController.Add(request);

            return new IncomeSource
            {
                Id = response.Id,
                Name = response.Name,
                Sequence = response.Sequence
            };
        }

        public int Modify(IncomeSource data)
        {
            var request = new IncomeSourceUpdateRequest
            {
                Id = data.Id,
                Name = data.Name,
                Sequence = data.Sequence
            };
            int response = _incomeSourceController.Modify(request);

            return response;
        }

        public IEnumerable<IncomeSource> ReadAll()
        {
            IEnumerable<IncomeSourceResponse> incomeSourceResponses = _incomeSourceController.ReadAll();
            int count = incomeSourceResponses.Count();
            var incomeSources = new List<IncomeSource>(count);

            foreach (IncomeSourceResponse response in incomeSourceResponses)
            {
                var Source = new IncomeSource
                {
                    Id = response.Id,
                    Name = response.Name,
                    Sequence = response.Sequence
                };
                incomeSources.Add(Source);
            }

            return incomeSources;
        }

        public int Remove(IncomeSource data)
        {
            return _incomeSourceController.Delete(data.Id);
        }
    }
}
