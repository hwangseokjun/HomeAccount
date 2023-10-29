using HomeAccountDB.Dtos;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountDB.Controllers
{
    public class IncomeCategoryController
    {
        private readonly IncomeCategoryService _incomeCategoryService;

        public IncomeCategoryController(IncomeCategoryService incomeCategoryService)
        {
            _incomeCategoryService = incomeCategoryService;
        }

        public IEnumerable<IncomeCategoryResponse> ReadAll()
        {
            return _incomeCategoryService.GetAll();
        }

        public IncomeCategoryResponse Add(IncomeCategorySaveRequest incomeCategorySaveRequest)
        {
            return _incomeCategoryService.Save(incomeCategorySaveRequest);
        }

        public int Delete(int id)
        {
            return _incomeCategoryService.Remove(id);
        }

        public int Modify(IncomeCategoryUpdateRequest incomeCategoryUpdateRequest)
        {
            return _incomeCategoryService.Update(incomeCategoryUpdateRequest);
        }
    }
}
