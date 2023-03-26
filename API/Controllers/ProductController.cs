
using Core.Entities;
using Core.interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;
        public ProductController(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _iProductRepository.GetProductsAsync();
            return Ok(products);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var product = await _iProductRepository.GetProductsByIdAsync(id);
            return product;

        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var brands = await _iProductRepository.GetProductBrandsAsync();
            return Ok(brands);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var types = await _iProductRepository.GetProductTypesAsync();
            return Ok(types);

        }

    }
}
