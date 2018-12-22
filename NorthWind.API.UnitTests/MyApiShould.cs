using System;
using System.Threading.Tasks;
using Microsoft.Rest;
using MyAPI;
using Xunit;

namespace NorthWind.API.UnitTests
{
    public class MyApiShould
    {
        readonly Uri _baseUr = new Uri("http://localhost:59152");

        [Fact]
        public async Task GetCategoryItemMust()
        {
            var subjectUnderTest = new MyAPIClient(_baseUr, new BasicAuthenticationCredentials());

            //// ACT
            var result = await subjectUnderTest.GetCategoryItemWithHttpMessagesAsync(1);

            //// ASSERT
            Assert.True(result.Response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetProductItemMust()
        {
            var subjectUnderTest = new MyAPIClient(_baseUr, new BasicAuthenticationCredentials());

            //// ACT
            var result = await subjectUnderTest.GetProductItemWithHttpMessagesAsync(1);

            //// ASSERT
            Assert.True(result.Response.IsSuccessStatusCode);
        }
    }
}
