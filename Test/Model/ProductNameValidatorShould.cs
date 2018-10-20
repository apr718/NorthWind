using System;
using NorthWind.Models;
using Xunit;

namespace Test.Model
{
    public class ProductNameValidatorShould
    {
        [Theory]
        [InlineData("axt")]
        [InlineData("adfax")]
        public void AcceptValidSchemas(string productName)
        {
            //sut -> system uder test
            var sut = new ProductNameValidator();


            Assert.True(sut.IsValid(productName));
        }

        [Theory]
        [InlineData("axqwerwqerwqerfghsdfghs kjhsalkfdgjhslkjfg kjhsdfghkjh dsafadsfasdf")]
        [InlineData("ax")]

        public void RejectedInvalidSchemas(string productName)
        {
            var sut = new ProductNameValidator();

            Assert.False(sut.IsValid(productName));
        }

        [Fact]
        public void ThrowExceptionWhenNullProductName()
        {
            var sut = new ProductNameValidator();

            Assert.Throws<ArgumentNullException>(()=> (sut.IsValid(null)) );
        }
    }
}
