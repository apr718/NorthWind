using Models;
using NorthWind.Models;
using Xunit;
using Moq;
using NorthWind.Core.Interfaces;

namespace Test.Model
{
    public class ProductEvaluatorShould
    {
        private readonly Mock<IProductNameValidator> _mockValidator;
        private readonly ProductEvaluator _sut;

        public ProductEvaluatorShould()
        {
            _mockValidator = new Mock<IProductNameValidator>();
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            _sut = new ProductEvaluator(_mockValidator.Object);
        }

        [Theory]
        [InlineData(0.5, 10, 3)]
        [InlineData(0.2, 12, 4)]
        [InlineData(0.3, 99, 5)]
        public void AutoAcceptProduct(float price, short stock, short units)
        {
            var prod = new Product
            {
                UnitPrice = (decimal)price,
                UnitsInStock = stock,
                UnitsOnOrder = units
            };

            Assert.Equal(ProductDecision.AutoAccepted, _sut.Evaluate(prod));
        }

        [Theory]
        [InlineData(0.1, 10, 3)]
        [InlineData(0.02, 12, 4)]
        [InlineData(0.03, 99, 5)]
        public void LowProductPrice(float price, short stock, short units)
        {
            var prod = new Product
            {
                UnitPrice = (decimal)price,
                UnitsInStock = stock,
                UnitsOnOrder = units
            };

            Assert.Equal(ProductDecision.MinPriceLimit, _sut.Evaluate(prod));
        }


        [Theory]
        [InlineData(0.5, 1, 3)]
        [InlineData(0.2, 2, 4)]
        [InlineData(0.3, 3, 5)]
        public void ProductMinTreshhold(float price, short stock, short units)
        {
            var prod = new Product
            {
                UnitPrice = (decimal)price,
                UnitsInStock = stock,
                UnitsOnOrder = units
            };

            Assert.Equal(ProductDecision.MinTreshhold, _sut.Evaluate(prod));
        }

        [Theory]
        [InlineData(0.5, 0, 3)]
        [InlineData(0.2, -20, 4)]
        [InlineData(0.3, -3, 5)]
        public void ProductOutOfStock(float price, short stock, short units)
        {
            var prod = new Product
            {
                UnitPrice = (decimal)price,
                UnitsInStock = stock,
                UnitsOnOrder = units
            };

            Assert.Equal(ProductDecision.OutOfStock, _sut.Evaluate(prod));
        }

        [Theory]
        [InlineData(0.5, 101, 3)]
        [InlineData(0.2, 2000, 4)]
        [InlineData(0.3, short.MaxValue, 5)]
        public void ProductStockOverload(float price, short stock, short units)
        {
           var prod = new Product
            {
                UnitPrice = (decimal)price,
                UnitsInStock = stock,
                UnitsOnOrder = units
            };

            Assert.Equal(ProductDecision.StockOverload, _sut.Evaluate(prod));
        }

        [Fact]
        public void WrongProductName()
        {
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
            
            var prod = new Product();

            Assert.Equal(ProductDecision.WrongProductName, _sut.Evaluate(prod));

            _mockValidator.Verify(x=>x.IsValid(It.IsAny<string>()), Times.Once);
        }
    }
}
