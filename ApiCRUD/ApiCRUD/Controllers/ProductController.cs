using Application.Interface;
using AutoMapper;
using Common.ViewModel;
using Core.Interface.Repositories;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IManagementProduct _managementProduct;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IManagementProduct ManagementProduct,
            IMapper mapper,
            ILogger<ProductController> logger)
        {
            _managementProduct = ManagementProduct;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _managementProduct.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _managementProduct.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogInformation($"No existen productos con el id: {id}");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _managementProduct.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductEdit productEdit)
        {
            var product = await _managementProduct.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogInformation($"No existen productos con el id: {id}");
                return NotFound();
            }

            try
            {
                product = _mapper.Map<Product>(productEdit);
                product.Id = id;

                await _managementProduct.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException);
                return Problem(ex.Message);
            }

            return Ok("Producto actualizado exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _managementProduct.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _managementProduct.DeleteAsync(id);

            return Ok("Producto eliminado exitosamente");
        }
    }
}
