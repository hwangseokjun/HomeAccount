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
    public class IncomeSourceServiceTests
    {
        private List<IncomeSource> _mockData;
        private Mock<IIncomeSourceRepository> _mockIncomeSourceRepository;
        private IncomeSourceService _incomeSourceService;

        [TestInitialize()]
        public void Initialize()
        {
            _mockData = new List<IncomeSource>();
            _mockIncomeSourceRepository = new Mock<IIncomeSourceRepository>();

            // SetGetAll
            _ = _mockIncomeSourceRepository
                .Setup(m => m.GetAll())
                .Returns(_mockData);

            // SetInsert
            _ = _mockIncomeSourceRepository
                .Setup(m => m.Insert(It.IsAny<IncomeSource>()))
                .Callback((IncomeSource source) => _mockData.Add(source))
                .Returns(_mockData.Count() + 1);

            // SetGetById
            _ = _mockIncomeSourceRepository
                .Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => _mockData.FirstOrDefault(d => d.Id == id));

            // SetDelete
            _ = _mockIncomeSourceRepository
                .Setup(m => m.Delete(It.IsAny<IncomeSource>()))
                .Callback((IncomeSource source) => _mockData.Remove(_mockData.FirstOrDefault(d => d.Id == source.Id)))
                .Returns<IncomeSource>(source => _mockData.FirstOrDefault(d => d.Id == source.Id) == null);

            // SetUpdate
            _ = _mockIncomeSourceRepository
                .Setup(m => m.Update(It.IsAny<IncomeSource>()))
                .Callback((IncomeSource source) =>
                {
                    var mockSource = _mockData.FirstOrDefault(d => d.Id == source.Id);
                    mockSource.Name = source.Name;
                    mockSource.Sequence = source.Sequence;
                })
                .Returns<IncomeSource>(source => _mockData.FirstOrDefault(d => d.Id == source.Id) != null);

            _incomeSourceService = new IncomeSourceService(_mockIncomeSourceRepository.Object);
        }

        [TestMethod("데이터 모두 읽기")]
        public void GetAllTest()
        {
            // Arrange
            _mockData.Clear();
            var incomeMethod = new IncomeSource { Id = 1, Name = "회사", Sequence = 1, IsDeleted = false };
            _mockData.Add(incomeMethod);

            // Act
            IEnumerable<IncomeSourceResponse> incomeSourceResponses = _incomeSourceService.GetAll();

            // Assert
            Assert.AreEqual($"1, 회사, 1, False", incomeSourceResponses.First().ToString());
        }

        [TestMethod("데이터 추가")]
        public void AddTest()
        {
            // Arrange
            var incomeSourceSaveRequest = new IncomeSourceSaveRequest
            {
                Name = "아르바이트",
                Sequence = 1
            };

            // Act
            IncomeSourceResponse incomeSourceResponse = _incomeSourceService.Save(incomeSourceSaveRequest);

            // Assert
            Assert.AreEqual($"1, 아르바이트, 1, False", incomeSourceResponse.ToString());
        }

        [TestMethod("데이터 삭제")]
        public void RemoveTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new IncomeSource { Id = 1, Name = "회사", Sequence = 1, IsDeleted = false });
            _mockData.Add(new IncomeSource { Id = 2, Name = "아르바이트", Sequence = 2, IsDeleted = false });
            _mockData.Add(new IncomeSource { Id = 3, Name = "격려금", Sequence = 3, IsDeleted = false });

            // Act
            _incomeSourceService.Remove(1);

            // Assert
            IEnumerable<IncomeSourceResponse> incomeSourceResponses = _incomeSourceService.GetAll();
            Assert.AreEqual(2, incomeSourceResponses.Count());
        }

        [TestMethod("데이터 수정")]
        public void UpdateTest()
        {
            // Arrange
            _mockData.Clear();
            _mockData.Add(new IncomeSource { Id = 1, Name = "회사", Sequence = 1, IsDeleted = false });
            _mockData.Add(new IncomeSource { Id = 2, Name = "dkfmqkdlxm", Sequence = 2, IsDeleted = false });
            _mockData.Add(new IncomeSource { Id = 3, Name = "격려금", Sequence = 3, IsDeleted = false });
            var incomeSourceUpdateRequest = new IncomeSourceUpdateRequest { Id = 2, Name = "아르바이트", Sequence = 2 };

            // Act
            int id = _incomeSourceService.Update(incomeSourceUpdateRequest);

            // Assert
            string actual = new IncomeSourceResponse(_mockData.FirstOrDefault(d => d.Id == id)).ToString();
            Assert.AreEqual("2, 아르바이트, 2, False", actual);
        }
    }
}