using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Core.Services;
using ApiBuildDemo.Infrastructure.Interfaces;
using ApiBuildDemo.Infrastructure.Models;
using Moq;
using Xunit;

namespace ApiBuildDemo.UnitTests.ValueServiceTest {
    public class GetValuesAsync {
        private readonly Mock<ILoggerAdapter<ValueService>> _loggerAdapterMock;
        private readonly Mock<IValueRepository> _valueRepositoryMock;
        private readonly List<Mock> _mockList;
        private readonly ValueService _sut;
        public GetValuesAsync () {

            _loggerAdapterMock = new Mock<ILoggerAdapter<ValueService>> (MockBehavior.Strict);
            _valueRepositoryMock = new Mock<IValueRepository> (MockBehavior.Strict);

            _mockList = new List<Mock> {
                _loggerAdapterMock,
                _valueRepositoryMock
            };
            _sut = new ValueService (_loggerAdapterMock.Object, _valueRepositoryMock.Object);
        }

        [Fact]
        public async Task When_ValuesIsCalled_Expect_AllValuesToRepository () {
            // Arrange
            var expectValues = new List<Value>{
              new Value{
                  Id = new Guid(),
                  DateCreated = new DateTime(),
                  DateModified = new DateTime(),
                  Title = "Title",
                  Message = "Message"
              }  
            };

            // Expectations
            _valueRepositoryMock.Setup(x => x.FindAll()).Returns(Task.FromResult(expectValues));

            // Act
            var result = await _sut.GetValuesAsync();

            // Assert
            Assert.Equal(result.Count,1);
            Assert.Equal(result, expectValues);
        }
    }
}