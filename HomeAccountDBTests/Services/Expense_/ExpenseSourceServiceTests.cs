using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAccountDB.Repository;
using HomeAccountDB.Models;
using Moq;
using HomeAccountDB.Dtos;

namespace HomeAccountDB.Services.Tests
{
    [TestClass()]
    public class ExpenseSourceServiceTests
    {
        private List<ExpenseSource> _mockData;
        private Mock<IExpenseSourceRepository> _mockExpenseSourceRepository;
        private ExpenseSourceService _expenseSourceService;

        [TestInitialize()]
        public void Initialize()
        {
            _mockData = new List<ExpenseSource>();
            _mockExpenseSourceRepository = new Mock<IExpenseSourceRepository>();

            // SetGetAll
            _ = _mockExpenseSourceRepository
                .Setup(m => m.GetAll())
                .Returns(_mockData);

            // SetInsert
            _ = _mockExpenseSourceRepository
                .Setup(m => m.Insert(It.IsAny<ExpenseSource>()))
                .Callback((ExpenseSource source) => _mockData.Add(source))
                .Returns(_mockData.Count() + 1);

            // SetGetById
            _ = _mockExpenseSourceRepository
                .Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => _mockData.FirstOrDefault(d => d.Id == id));

            // SetDelete
            _ = _mockExpenseSourceRepository
                .Setup(m => m.Delete(It.IsAny<ExpenseSource>()))
                .Callback((ExpenseSource source) => _mockData.Remove(_mockData.FirstOrDefault(d => d.Id == source.Id)))
                .Returns<ExpenseSource>(source => _mockData.FirstOrDefault(d => d.Id == source.Id) == null);

            // SetUpdate
            _ = _mockExpenseSourceRepository
                .Setup(m => m.Update(It.IsAny<ExpenseSource>()))
                .Callback((ExpenseSource source) =>
                {
                    var mockSource = _mockData.FirstOrDefault(d => d.Id == source.Id);
                    mockSource.Name = source.Name;
                    mockSource.Sequence = source.Sequence;
                })
                .Returns<ExpenseSource>(source => _mockData.FirstOrDefault(d => d.Id == source.Id) != null);

            _expenseSourceService = new ExpenseSourceService(_mockExpenseSourceRepository.Object);
        }

        [TestMethod("데이터 모두 읽기")]
        public void GetAllTest()
        {
            // Arrange
            _mockData.Clear();
            var expenseMethod = new ExpenseSource { Id = 1, Name = "이케아", Sequence = 1, IsDeleted = false };
            _mockData.Add(expenseMethod);

            // Act
            IEnumerable<ExpenseSourceResponse> expenseSourceResponses = _expenseSourceService.GetAll();

            // Assert
            Assert.AreEqual($"1, 이케아, 1, False", expenseSourceResponses.First().ToString());
        }

        [TestMethod("데이터 추가")]
        public void AddTest()
        {
            // Arrange
            var expenseSourceSaveRequest = new ExpenseSourceSaveRequest
            {
                Name = "또랑국밥",
                Sequence = 1
            };

            // Act
            ExpenseSourceResponse expenseSourceResponse = _expenseSourceService.Save(expenseSourceSaveRequest);

            // Assert
            Assert.AreEqual($"1, 또랑국밥, 1, False", expenseSourceResponse.ToString());
        }

        [TestMethod("데이터 삭제")]
        public void RemoveTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new ExpenseSource { Id = 1, Name = "이케아", Sequence = 1, IsDeleted = false });
            _mockData.Add(new ExpenseSource { Id = 2, Name = "또랑국밥", Sequence = 2, IsDeleted = false });
            _mockData.Add(new ExpenseSource { Id = 3, Name = "맑은샘병원", Sequence = 3, IsDeleted = false });

            // Act
            _expenseSourceService.Remove(1);

            // Assert
            IEnumerable<ExpenseSourceResponse> expenseSourceResponses = _expenseSourceService.GetAll();
            Assert.AreEqual(2, expenseSourceResponses.Count());
        }

        [TestMethod("데이터 수정")]
        public void UpdateTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new ExpenseSource { Id = 1, Name = "이케아", Sequence = 1, IsDeleted = false });
            _mockData.Add(new ExpenseSource { Id = 2, Name = "Ehfkdrnrqkq", Sequence = 2, IsDeleted = false });
            _mockData.Add(new ExpenseSource { Id = 3, Name = "맑은샘병원", Sequence = 3, IsDeleted = false });
            var expenseSourceUpdateRequest = new ExpenseSourceUpdateRequest { Id = 2, Name = "또랑국밥", Sequence = 2 };

            // Act
            int id = _expenseSourceService.Update(expenseSourceUpdateRequest);

            // Assert
            string actual = new ExpenseSourceResponse(_mockData.FirstOrDefault(d => d.Id == id)).ToString();
            Assert.AreEqual("2, 또랑국밥, 2, False", actual);
        }
    }
}