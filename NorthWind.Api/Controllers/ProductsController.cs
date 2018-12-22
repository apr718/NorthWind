using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using NorthWind.Api.Service.Interfaces;
using Services.Interfaces;

namespace NorthWind.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// ProductsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IBLService _bLService;
        private readonly IConfigurationService _configurationService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bLService"></param>
        /// <param name="configurationService"></param>
        public ProductsController(IBLService bLService, IConfigurationService configurationService)
        {
            _bLService = bLService;
            _configurationService = configurationService;
        }
        /// <summary>
        /// Get method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            int pageSize = _configurationService.PageSize;
            var products = await _bLService.GetAllProductsAsync(pageSize);
            return Ok(products);
        }
        /// <summary>
        /// GetbyId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductItem(int id)
        {
            var todoItem = await _bLService.GetProductDetailsAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="productItem"></param>
        /// <returns></returns>
       [HttpPut]
       [ProducesResponseType((int)HttpStatusCode.BadRequest)]
       [ProducesResponseType((int)HttpStatusCode.NotFound)]
       [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Update(Product productItem)
        {
            await _bLService.AddOrUpdateProductAsync(productItem);

            return CreatedAtAction("GetProductItem", new { id = productItem.ProductID }, productItem);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Delete([FromQuery][Required]int id)
        {
            var todoItem = await _bLService.GetProductDetailsAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            await _bLService.DeleteProductAsync(id);

            return todoItem;
        }
    }
}