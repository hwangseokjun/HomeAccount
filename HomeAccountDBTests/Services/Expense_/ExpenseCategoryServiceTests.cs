using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using HomeAccountDB.Repository;
using HomeAccountDB.Models;
using HomeAccountDB.Dtos;

namespace HomeAccountDB.Services.Tests
{
    [TestClass()]
    public class ExpenseCategoryServiceTests
    {
        private List<ExpenseCategory> _mockData;
        private Mock<IExpenseCategoryRepository> _mockExpenseCategoryRepository;
        private ExpenseCategoryService _expenseCategoryService;

        [TestInitialize()]
        public void Initialize()
        {
            _mockData = new List<ExpenseCategory>();
            _mockExpenseCategoryRepository = new Mock<IExpenseCategoryRepository>();

            // SetGetAll
            _ = _mockExpenseCategoryRepository
                .Setup(m => m.GetAll())
                .Returns(_mockData);

            // SetInsert
            _ = _mockExpenseCategoryRepository
                .Setup(m => m.Insert(It.IsAny<ExpenseCategory>()))
                .Callback((ExpenseCategory category) => _mockData.Add(category))
                .Returns(_mockData.Count() + 1);

            // SetGetById
            _ = _mockExpenseCategoryRepository
                .Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => _mockData.FirstOrDefault(d => d.Id == id));

            // SetDelete
            _ = _mockExpenseCategoryRepository
                .Setup(m => m.Delete(It.IsAny<ExpenseCategory>()))
                .Callback((ExpenseCategory category) => _mockData.Remove(_mockData.FirstOrDefault(d => d.Id == category.Id)))
                .Returns<ExpenseCategory>(category => _mockData.FirstOrDefault(d => d.Id == category.Id) == null);

            // SetUpdate
            _ = _mockExpenseCategoryRepository
                .Setup(m => m.Update(It.IsAny<ExpenseCategory>()))
                .Callback((ExpenseCategory category) =>
                {
                    var mockCategory = _mockData.FirstOrDefault(d => d.Id == category.Id);
                    mockCategory.Name = category.Name;
                    mockCategory.Sequence = category.Sequence;
                })
                .Returns<ExpenseCategory>(category => _mockData.FirstOrDefault(d => d.Id == category.Id) != null);

            _expenseCategoryService = new ExpenseCategoryService(_mockExpenseCategoryRepository.Object);
        }

        [TestMethod("데이터 모두 읽기")]
        public void GetAllTest()
        {
            // Arrange
            _mockData.Clear();
            var expenseCategory = new ExpenseCategory { Id = 1, Name = "식비", Sequence = 1, IsDeleted = false };
            _mockData.Add(expenseCategory);

            // Act
            IEnumerable<ExpenseCategoryResponse> expenseCategoryResponses = _expenseCategoryService.GetAll();

            // Assert
            Assert.AreEqual($"1, 식비, 1, False", expenseCategoryResponses.First().ToString());
        }

        [TestMethod("데이터 추가")]
        public void AddTest()
        {
            // Arrange
            var expenseCategorySaveRequest = new ExpenseCategorySaveRequest
            {
                Name = "교통비",
                Sequence = 1
            };
            
            // Act
            ExpenseCategoryResponse expenseCategoryResponse = _expenseCategoryService.Save(expenseCategorySaveRequest);

            // Assert
            Assert.AreEqual($"1, 교통비, 1, False", expenseCategoryResponse.ToString());
        }

        [TestMethod("데이터 삭제")]
        public void RemoveTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new ExpenseCategory { Id = 1, Name = "식비", Sequence = 1, IsDeleted = false });
            _mockData.Add(new ExpenseCategory { Id = 2, Name = "교통비", Sequence = 2, IsDeleted = false });
            _mockData.Add(new ExpenseCategory { Id = 3, Name = "생활비", Sequence = 3, IsDeleted = false });

            // Act
            _expenseCategoryService.Remove(1);

            // Assert
            IEnumerable<ExpenseCategoryResponse> expenseCategoryResponses = _expenseCategoryService.GetAll();
            Assert.AreEqual(2, expenseCategoryResponses.Count());
        }

        [TestMethod("데이터 수정")]
        public void UpdateTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new ExpenseCategory { Id = 1, Name = "식비", Sequence = 1, IsDeleted = false });
            _mockData.Add(new ExpenseCategory { Id = 2, Name = "ryxhdql", Sequence = 2, IsDeleted = false });
            _mockData.Add(new ExpenseCategory { Id = 3, Name = "생활비", Sequence = 3, IsDeleted = false });
            var expenseCategoryUpdateRequest = new ExpenseCategoryUpdateRequest { Id = 2, Name = "교통비", Sequence = 2 };

            // Act
            int id = _expenseCategoryService.Update(expenseCategoryUpdateRequest);

            // Assert
            string actual = new ExpenseCategoryResponse(_mockData.FirstOrDefault(d => d.Id == id)).ToString();
            Assert.AreEqual("2, 교통비, 2, False", actual);
        }
    }
}