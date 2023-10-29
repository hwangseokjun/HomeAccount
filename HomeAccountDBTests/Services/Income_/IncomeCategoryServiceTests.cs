using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAccountDB.Models;
using HomeAccountDB.Repository;
using Moq;
using HomeAccountDB.Dtos;

namespace HomeAccountDB.Services.Tests
{
    [TestClass()]
    public class IncomeCategoryServiceTests
    {
        private List<IncomeCategory> _mockData;
        private Mock<IIncomeCategoryRepository> _mockIncomeCategoryRepository;
        private IncomeCategoryService _incomeCategoryService;

        [TestInitialize()]
        public void Initialize()
        {
            _mockData = new List<IncomeCategory>();
            _mockIncomeCategoryRepository = new Mock<IIncomeCategoryRepository>();

            // SetGetAll
            _ = _mockIncomeCategoryRepository
                .Setup(m => m.GetAll())
                .Returns(_mockData);

            // SetInsert
            _ = _mockIncomeCategoryRepository
                .Setup(m => m.Insert(It.IsAny<IncomeCategory>()))
                .Callback((IncomeCategory category) => _mockData.Add(category))
                .Returns(_mockData.Count() + 1);

            // SetGetById
            _ = _mockIncomeCategoryRepository
                .Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => _mockData.FirstOrDefault(d => d.Id == id));

            // SetDelete
            _ = _mockIncomeCategoryRepository
                .Setup(m => m.Delete(It.IsAny<IncomeCategory>()))
                .Callback((IncomeCategory category) => _mockData.Remove(_mockData.FirstOrDefault(d => d.Id == category.Id)))
                .Returns<IncomeCategory>(category => _mockData.FirstOrDefault(d => d.Id == category.Id) == null);

            // SetUpdate
            _ = _mockIncomeCategoryRepository
                .Setup(m => m.Update(It.IsAny<IncomeCategory>()))
                .Callback((IncomeCategory category) =>
                {
                    var mockCategory = _mockData.FirstOrDefault(d => d.Id == category.Id);
                    mockCategory.Name = category.Name;
                    mockCategory.Sequence = category.Sequence;
                })
                .Returns<IncomeCategory>(category => _mockData.FirstOrDefault(d => d.Id == category.Id) != null);

            _incomeCategoryService = new IncomeCategoryService(_mockIncomeCategoryRepository.Object);
        }

        [TestMethod("데이터 모두 읽기")]
        public void GetAllTest()
        {
            // Arrange
            _mockData.Clear();
            var incomeCategory = new IncomeCategory { Id = 1, Name = "월급", Sequence = 1, IsDeleted = false };
            _mockData.Add(incomeCategory);

            // Act
            IEnumerable<IncomeCategoryResponse> incomeCategoryResponses = _incomeCategoryService.GetAll();

            // Assert
            Assert.AreEqual($"1, 월급, 1, False", incomeCategoryResponses.First().ToString());
        }

        [TestMethod("데이터 추가")]
        public void AddTest()
        {
            // Arrange
            var incomeCategorySaveRequest = new IncomeCategorySaveRequest
            {
                Name = "축의금",
                Sequence = 1
            };

            // Act
            IncomeCategoryResponse incomeCategoryResponse = _incomeCategoryService.Save(incomeCategorySaveRequest);

            // Assert
            Assert.AreEqual($"1, 축의금, 1, False", incomeCategoryResponse.ToString());
        }

        [TestMethod("데이터 삭제")]
        public void RemoveTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new IncomeCategory { Id = 1, Name = "월급", Sequence = 1, IsDeleted = false });
            _mockData.Add(new IncomeCategory { Id = 2, Name = "축의금", Sequence = 2, IsDeleted = false });
            _mockData.Add(new IncomeCategory { Id = 3, Name = "선물", Sequence = 3, IsDeleted = false });

            // Act
            _incomeCategoryService.Remove(1);

            // Assert
            IEnumerable<IncomeCategoryResponse> incomeCategoryResponses = _incomeCategoryService.GetAll();
            Assert.AreEqual(2, incomeCategoryResponses.Count());
        }

        [TestMethod("데이터 수정")]
        public void UpdateTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new IncomeCategory { Id = 1, Name = "월급", Sequence = 1, IsDeleted = false });
            _mockData.Add(new IncomeCategory { Id = 2, Name = "cnrdmlrma", Sequence = 2, IsDeleted = false });
            _mockData.Add(new IncomeCategory { Id = 3, Name = "선물", Sequence = 3, IsDeleted = false });
            var incomeCategoryUpdateRequest = new IncomeCategoryUpdateRequest { Id = 2, Name = "축의금", Sequence = 2 };

            // Act
            int id = _incomeCategoryService.Update(incomeCategoryUpdateRequest);

            // Assert
            string actual = new IncomeCategoryResponse(_mockData.FirstOrDefault(d => d.Id == id)).ToString();
            Assert.AreEqual("2, 축의금, 2, False", actual);
        }
    }
}