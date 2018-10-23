using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using NorthWind.Controllers;
using NorthWind.ViewModels;
using Services.Interfaces;
using UI.Services.Interfaces;
using Xunit;

namespace Test.Controller
{
    public class ProductsControllerShould
    {
        private readonly Mock<IBLService> _mockService;
        private readonly Mock<IConfigurationService> _mockConvig;
        private readonly ProductsController _sut;

        public ProductsControllerShould()
        {
            _mockService = new Mock<IBLService>();
            _mockConvig = new Mock<IConfigurationService>();
            _sut = new ProductsController(_mockService.Object, _mockConvig.Object);
        }
        [Fact]
        public void ReturnViewForIndex()
        {
            Task<IActionResult> result = _sut.Index();

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public async Task ReturnViewWhenInvalidModelStateForCreate()
        {
            _sut.ModelState.AddModelError("x", "Test error");

            var prod = new InMemoryProductData
            {
                ProductName = "vova"
            };

            IActionResult result = await _sut.Create(prod);

            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsType<InMemoryProductData>(viewResult.Model);

            Assert.Equal(prod.ProductName, model.ProductName);
        }

        [Fact]
        public async Task NotSaveProductWhenModelErrorCreate()
        {
            _sut.ModelState.AddModelError("x", "Test error");

            var prod = new InMemoryProductData
            {
                ProductName = "vova"
            };

            await _sut.Create(prod);

           _mockService.Verify(x=>x.AddOrUpdateProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task SaveProductWhenValidModelCreateEdit()
        {
            Product savedProduct = null;

            _mockService.Setup(x => x.AddOrUpdateProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask)
                .Callback<Product>(x => savedProduct = x);

            var prod = new InMemoryProductData
            {
                ProductName = "Vasia",
                UnitsInStock = 15,
                UnitPrice = 10m,
                Discontinued = false,
                QuantityPerUnit = "10x10",
                UnitsOnOrder = 2,
                ProductId = 1,
                SelectedCategoryId = 1,
                SelectedSupplierId = 1
            };

            await _sut.Create(prod);

            _mockService.Verify(
                x=>x.AddOrUpdateProductAsync(It.IsAny<Product>()), Times.Once);

            Assert.Equal(prod.ProductName, savedProduct.ProductName);
            Assert.Equal(prod.UnitsInStock, savedProduct.UnitsInStock);
            Assert.Equal(prod.UnitPrice, savedProduct.UnitPrice);
            Assert.Equal(prod.Discontinued, savedProduct.Discontinued);
            Assert.Equal(prod.QuantityPerUnit, savedProduct.QuantityPerUnit);
            Assert.Equal(prod.UnitsOnOrder, savedProduct.UnitsOnOrder);
            Assert.Equal(prod.ProductId, savedProduct.ProductID);
            Assert.Equal(prod.SelectedCategoryId, savedProduct.CategoryID);
            Assert.Equal(prod.SelectedSupplierId, savedProduct.SupplierID);
        }

        [Fact]
        public async Task DeleteProductMust()
        {
            await _sut.Delete(null);

            _mockService.Verify(x => x.DeleteProductAsync(It.IsAny<int>()), Times.Never);
        }


        [Fact]
        public async Task ReturnViewForDetails()
        {
            await _sut.Details(It.IsAny<int>());

            _mockService.Verify(x => x.GetProductDetailsAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void ReturnViewForDeleteConfirmed()
        {
            Task<IActionResult> result = _sut.DeleteConfirmed(It.IsAny<int>());

            var res = Assert.IsType<Task<IActionResult>>(result);

            var viewResult = res.Result as RedirectToActionResult;

            Assert.Equal("Index", viewResult.ActionName);
        }
    }
}
