using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using NorthWind.Api.Service.Interfaces;
using Services.Interfaces;

namespace NorthWind.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// CategoriesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly IBLService _bLService;
        private readonly IConfigurationService _configurationService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bLService"></param>
        /// <param name="configurationService"></param>
        public CategoriesController(IBLService bLService, IConfigurationService configurationService)
        {
            _bLService = bLService;
            _configurationService = configurationService;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            int pageSize = _configurationService.PageSize;
            var categories = await _bLService.GetAllCategoriesAsync(pageSize);
            return Ok(categories);
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> GetCategoryItem(int id)
        {
            var todoItem = await _bLService.GetCategoryDetailsAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="categoryItem"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> Post([FromBody]Category categoryItem)
        {
            await _bLService.AddOrUpdateCategoryAsync(categoryItem);

            return CreatedAtAction("GetCategoryItem", new { id = categoryItem.CategoryId }, categoryItem);
        }
    }
}

