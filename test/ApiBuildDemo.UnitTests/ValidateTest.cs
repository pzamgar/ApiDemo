using System;
using Xunit;

namespace ApiBuildDemo.UnitTests
{
    public class ValidateTest
    {
        [Fact]
        public void When_RunTests_Expect_TestOk()
        {
            // Arrange
            var num1 = 2;
            var num2 = 3;
            var expect = 5;

            // Act
            var result = num1+num2;

            // Assert
            Assert.Equal(result, expect);
        }
    }
}
