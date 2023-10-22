using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeAccountDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using HomeAccountDB.Models;
using HomeAccountDB.Repository;
using HomeAccountDB.Dtos;

namespace HomeAccountDB.Services.Tests
{
    [TestClass()]
    public class ExpenseMethodServiceTests
    {
        private List<ExpenseMethod> _mockData;
        private Mock<IExpenseMethodRepository> _mockExpenseMethodRepository;
        private ExpenseMethodService _expenseMethodService;

        [TestInitialize()]
        public void Initialize()
        {
            _mockData = new List<ExpenseMethod>();
            _mockExpenseMethodRepository = new Mock<IExpenseMethodRepository>();

            // SetGetAll
            _ = _mockExpenseMethodRepository
                .Setup(m => m.GetAll())
                .Returns(_mockData);

            // SetInsert
            _ = _mockExpenseMethodRepository
                .Setup(m => m.Insert(It.IsAny<ExpenseMethod>()))
                .Callback((ExpenseMethod method) => _mockData.Add(method))
                .Returns(_mockData.Count() + 1);

            // SetGetById
            _ = _mockExpenseMethodRepository
                .Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => _mockData.FirstOrDefault(d => d.Id == id));

            // SetDelete
            _ = _mockExpenseMethodRepository
                .Setup(m => m.Delete(It.IsAny<ExpenseMethod>()))
                .Callback((ExpenseMethod method) => _mockData.Remove(_mockData.FirstOrDefault(d => d.Id == method.Id)))
                .Returns(true);

            // SetUpdate
            _ = _mockExpenseMethodRepository
                .Setup(m => m.Update(It.IsAny<ExpenseMethod>()))
                .Callback((ExpenseMethod method) =>
                {
                    var mockMethod = _mockData.FirstOrDefault(d => d.Id == method.Id);
                    mockMethod.Name = method.Name;
                    mockMethod.Sequence = method.Sequence;
                })
                .Returns<ExpenseMethod>(method => _mockData.FirstOrDefault(d => d.Id == method.Id) != null);

            _expenseMethodService = new ExpenseMethodService(_mockExpenseMethodRepository.Object);
        }

        [TestMethod("데이터 모두 읽기")]
        public void GetAllTest()
        {
            // Arrange
            _mockData.Clear();
            var expenseMethod = new ExpenseMethod { Id = 1, Name = "현대카드", Sequence = 1, IsDeleted = false };
            _mockData.Add(expenseMethod);

            // Act
            IEnumerable<ExpenseMethodResponse> expenseMethodResponses = _expenseMethodService.GetAll();

            // Assert
            Assert.AreEqual($"1, 현대카드, 1, False", expenseMethodResponses.First().ToString());
        }

        [TestMethod("데이터 추가")]
        public void AddTest()
        {
            // Arrange
            var expenseMethodSaveRequest = new ExpenseMethodSaveRequest
            {
                Name = "교통비",
                Sequence = 1
            };

            // Act
            ExpenseMethodResponse expenseMethodResponse = _expenseMethodService.Save(expenseMethodSaveRequest);

            // Assert
            Assert.AreEqual($"1, 교통비, 1, False", expenseMethodResponse.ToString());
        }

        [TestMethod("데이터 삭제")]
        public void RemoveTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new ExpenseMethod { Id = 1, Name = "현대카드", Sequence = 1, IsDeleted = false });
            _mockData.Add(new ExpenseMethod { Id = 2, Name = "신한카드", Sequence = 2, IsDeleted = false });
            _mockData.Add(new ExpenseMethod { Id = 3, Name = "현금", Sequence = 3, IsDeleted = false });

            // Act
            _expenseMethodService.Remove(1);

            // Assert
            IEnumerable<ExpenseMethodResponse> expenseMethodResponses = _expenseMethodService.GetAll();
            Assert.AreEqual(2, expenseMethodResponses.Count());
        }

        [TestMethod("데이터 수정")]
        public void UpdateTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new ExpenseMethod { Id = 1, Name = "현대카드", Sequence = 1, IsDeleted = false });
            _mockData.Add(new ExpenseMethod { Id = 2, Name = "tlsgkszkem", Sequence = 2, IsDeleted = false });
            _mockData.Add(new ExpenseMethod { Id = 3, Name = "현금", Sequence = 3, IsDeleted = false });
            var expenseMethodUpdateRequest = new ExpenseMethodUpdateRequest { Id = 2, Name = "신한카드", Sequence = 2 };

            // Act
            int id = _expenseMethodService.Update(expenseMethodUpdateRequest);

            // Assert
            string actual = new ExpenseMethodResponse(_mockData.FirstOrDefault(d => d.Id == id)).ToString();
            Assert.AreEqual("2, 신한카드, 2, False", actual);
        }
    }
}