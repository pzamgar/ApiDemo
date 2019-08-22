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
    public class GetValueByIdAsync {
        private readonly Mock<ILoggerAdapter<ValueService>> _loggerAdapterMock;
        private readonly Mock<IValueRepository> _valueRepositoryMock;
        private readonly List<Mock> _mockList;
        private readonly ValueService _sut;
        public GetValueByIdAsync () {

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
            var guid = new Guid ();
            var expectValue = new Value {
                Id = guid,
                DateCreated = new DateTime (),
                DateModified = new DateTime (),
                Title = "Title",
                Message = "Message"
            };

            // Expectations
            _valueRepositoryMock.Setup (x => x.GetById (It.IsAny<Guid>())).Returns (Task.FromResult (expectValue));

            // Act
            var result = await _sut.GetValueByIdAsync (guid);

            // Assert
            Assert.Equal (result.Id, guid);
            Assert.NotNull(result.DateCreated);
            Assert.NotNull(result.DateModified);
            Assert.Equal(result.Title,expectValue.Title);
            Assert.Equal(result.Message,expectValue.Message);
        }
    }
}