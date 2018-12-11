using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Api.Service.Interfaces;
using Services.Interfaces;

namespace NorthWind.Api.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IBLService _bLService;
        private readonly IConfigurationService _configurationService;

        public ProductsController(IBLService bLService, IConfigurationService configurationService)
        {
            _bLService = bLService;
            _configurationService = configurationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int pageSize = _configurationService.PageSize;
            var products = await _bLService.GetAllProductsAsync(pageSize);
            return Ok(products);
        }
    }
}