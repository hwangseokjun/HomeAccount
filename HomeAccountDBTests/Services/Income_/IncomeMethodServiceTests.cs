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
    public class IncomeMethodServiceTests
    {
        private List<IncomeMethod> _mockData;
        private Mock<IIncomeMethodRepository> _mockIncomeMethodRepository;
        private IncomeMethodService _incomeMethodService;

        [TestInitialize()]
        public void Initialize()
        {
            _mockData = new List<IncomeMethod>();
            _mockIncomeMethodRepository = new Mock<IIncomeMethodRepository>();

            // SetGetAll
            _ = _mockIncomeMethodRepository
                .Setup(m => m.GetAll())
                .Returns(_mockData);

            // SetInsert
            _ = _mockIncomeMethodRepository
                .Setup(m => m.Insert(It.IsAny<IncomeMethod>()))
                .Callback((IncomeMethod method) => _mockData.Add(method))
                .Returns(_mockData.Count() + 1);

            // SetGetById
            _ = _mockIncomeMethodRepository
                .Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => _mockData.FirstOrDefault(d => d.Id == id));

            // SetDelete
            _ = _mockIncomeMethodRepository
                .Setup(m => m.Delete(It.IsAny<IncomeMethod>()))
                .Callback((IncomeMethod method) => _mockData.Remove(_mockData.FirstOrDefault(d => d.Id == method.Id)))
                .Returns<IncomeMethod>(method => _mockData.FirstOrDefault(d => d.Id == method.Id) == null);

            // SetUpdate
            _ = _mockIncomeMethodRepository
                .Setup(m => m.Update(It.IsAny<IncomeMethod>()))
                .Callback((IncomeMethod method) =>
                {
                    var mockMethod = _mockData.FirstOrDefault(d => d.Id == method.Id);
                    mockMethod.Name = method.Name;
                    mockMethod.Sequence = method.Sequence;
                })
                .Returns<IncomeMethod>(method => _mockData.FirstOrDefault(d => d.Id == method.Id) != null);

            _incomeMethodService = new IncomeMethodService(_mockIncomeMethodRepository.Object);
        }

        [TestMethod("데이터 모두 읽기")]
        public void GetAllTest()
        {
            // Arrange
            _mockData.Clear();
            var incomeMethod = new IncomeMethod { Id = 1, Name = "현대카드", Sequence = 1, IsDeleted = false };
            _mockData.Add(incomeMethod);

            // Act
            IEnumerable<IncomeMethodResponse> incomeMethodResponses = _incomeMethodService.GetAll();

            // Assert
            Assert.AreEqual($"1, 현대카드, 1, False", incomeMethodResponses.First().ToString());
        }

        [TestMethod("데이터 추가")]
        public void AddTest()
        {
            // Arrange
            var incomeMethodSaveRequest = new IncomeMethodSaveRequest
            {
                Name = "현금",
                Sequence = 1
            };

            // Act
            IncomeMethodResponse incomeMethodResponse = _incomeMethodService.Save(incomeMethodSaveRequest);

            // Assert
            Assert.AreEqual($"1, 현금, 1, False", incomeMethodResponse.ToString());
        }

        [TestMethod("데이터 삭제")]
        public void RemoveTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new IncomeMethod { Id = 1, Name = "현대카드", Sequence = 1, IsDeleted = false });
            _mockData.Add(new IncomeMethod { Id = 2, Name = "신한카드", Sequence = 2, IsDeleted = false });
            _mockData.Add(new IncomeMethod { Id = 3, Name = "현금", Sequence = 3, IsDeleted = false });

            // Act
            _incomeMethodService.Remove(1);

            // Assert
            IEnumerable<IncomeMethodResponse> incomeMethodResponses = _incomeMethodService.GetAll();
            Assert.AreEqual(2, incomeMethodResponses.Count());
        }

        [TestMethod("데이터 수정")]
        public void UpdateTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new IncomeMethod { Id = 1, Name = "현대카드", Sequence = 1, IsDeleted = false });
            _mockData.Add(new IncomeMethod { Id = 2, Name = "tlsgkszkem", Sequence = 2, IsDeleted = false });
            _mockData.Add(new IncomeMethod { Id = 3, Name = "현금", Sequence = 3, IsDeleted = false });
            var incomeMethodUpdateRequest = new IncomeMethodUpdateRequest { Id = 2, Name = "신한카드", Sequence = 2 };

            // Act
            int id = _incomeMethodService.Update(incomeMethodUpdateRequest);

            // Assert
            string actual = new IncomeMethodResponse(_mockData.FirstOrDefault(d => d.Id == id)).ToString();
            Assert.AreEqual("2, 신한카드, 2, False", actual);
        }
    }
}