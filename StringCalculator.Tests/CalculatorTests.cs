using StringCalculator;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_EmptyString_ReturnsZero()
        {
            // Arrange
            var input = "";

            // Act
            var result = _calculator.Add(input);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Add_SingleNumber_ReturnsThatNumber()
        {
            var result = _calculator.Add("5");
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Add_TwoNumbersSeparatedByComma_ReturnsSum()
        {
            var result = _calculator.Add("2,3");
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Add_MultipleNumbers_ReturnsSum()
        {
            var result = _calculator.Add("1,2,3,4,5");
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void Add_NumbersWithNewlineDelimiter_ReturnsSum()
        {
            var result = _calculator.Add("1\n2,3");
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_WithCustomDelimiter_ReturnsSum()
        {
            var result = _calculator.Add("//;\n1;2");
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Add_NegativeNumbers_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _calculator.Add("1,-2,3"));
            Assert.That(ex.Message, Does.Contain("Отрицательные числа запрещены: -2"));
        }

        [Test]
        public void Add_MultipleNegativeNumbers_ThrowsExceptionWithAllNegatives()
        {
            var ex = Assert.Throws<ArgumentException>(() => _calculator.Add("-1,2,-3"));
            Assert.That(ex.Message, Does.Contain("-1, -3"));
        }

        [Test]
        public void Add_NumbersGreaterThan1000_AreIgnored()
        {
            var result = _calculator.Add("2,1001");
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Add_MixedValidAndLargeNumbers_ReturnsSumOfValidOnly()
        {
            var result = _calculator.Add("5,1000,1001,10");
            Assert.That(result, Is.EqualTo(1015));
        }
    }
}