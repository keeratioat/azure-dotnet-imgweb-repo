using Web;
using Xunit;

namespace Calculator.Tests
{
    public class CalculatorTests
    {
        private readonly Web.Calculator _calc = new Web.Calculator();

        [Fact]
        public void Add_TwoIntegers_ReturnsSum()
        {
            var result = _calc.Add(3, 4);
            Assert.Equal(7, result);
        }

        [Fact]
        public void Subtract_TwoIntegers_ReturnsDifference()
        {
            var result = _calc.Subtract(10, 6);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Multiply_TwoIntegers_ReturnsProduct()
        {
            var result = _calc.Multiply(7, 5);
            Assert.Equal(35, result);
        }

        [Theory]
        [InlineData(10, 2, 5.0)]
        [InlineData(3, 2, 1.5)]
        [InlineData(1, 3, 0.3333333333333333)]
        public void Divide_ValidInputs_ReturnsQuotient(int a, int b, double expected)
        {
            var result = _calc.Divide(a, b);
            Assert.Equal(expected, result, 12); // precision
        }

        [Fact]
        public void Divide_ByZero_ReturnsZero()
        {
            var result = _calc.Divide(5, 0);
            Assert.Equal(0.0, result);
        }
    }
}
