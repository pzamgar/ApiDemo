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
    public class DeleteValueById {
        private readonly Mock<ILoggerAdapter<ValueService>> _loggerAdapterMock;
        private readonly Mock<IValueRepository> _valueRepositoryMock;
        private readonly List<Mock> _mockList;
        private readonly ValueService _sut;
        public DeleteValueById () {

            _loggerAdapterMock = new Mock<ILoggerAdapter<ValueService>> (MockBehavior.Strict);
            _valueRepositoryMock = new Mock<IValueRepository> (MockBehavior.Strict);

            _mockList = new List<Mock> {
                _loggerAdapterMock,
                _valueRepositoryMock
            };
            _sut = new ValueService (_loggerAdapterMock.Object, _valueRepositoryMock.Object);
        }

        [Fact]
        public async Task When_ValueByIdIsCalled_Expect_ValueWithIdCalled () {
            // Arrange
            var expectValue = new Value {
                Id = new Guid (),
                DateCreated = new DateTime (),
                DateModified = new DateTime (),
                Title = "Title",
                Message = "Message"
            };

            // Expectations
            _valueRepositoryMock.Setup (x => x.Create (It.IsAny<Value> ())).Returns (Task.FromResult (expectValue));

            // Act
            var result = await _sut.AddValueAsync (expectValue);

            // Assert
            Assert.Equal (result.Id, expectValue.Id);
            Assert.NotNull (result.DateCreated);
            Assert.NotNull (result.DateModified);
            Assert.Equal (result.Title, expectValue.Title);
            Assert.Equal (result.Message, expectValue.Message);
        }
    }
}