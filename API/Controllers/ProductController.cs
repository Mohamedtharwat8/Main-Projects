
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _iProductRepository;
        private readonly IGenericRepository<ProductBrand> _ProductBrandRepo;
        private readonly IGenericRepository<ProductType> _ProductTypeRepo;
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly Mapper _mapper;
        public ProductController(
            IProductRepository iProductRepository,
            IGenericRepository<ProductBrand> ProductBrandRepo,
            IGenericRepository<ProductType> ProductTypeRepo,
            IGenericRepository<Product> ProductRepo,
            Mapper mapper
            )
        {
            _iProductRepository = iProductRepository;
            _ProductBrandRepo = ProductBrandRepo;
            _ProductTypeRepo = ProductTypeRepo;
            _ProductRepo = ProductRepo;
            _mapper = mapper;


        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithFiltersForCountSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _ProductRepo.CountAsync(countSpec);

            var products = await _ProductRepo.ListAsync(spec);     //fetches product table from select query of database and stores it in variable

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProducts(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);

            var product = await _ProductRepo.GetEntityWithSpec(spec);
            return _mapper.Map<ProductToReturnDto>(product);
            //return new ProductToReturnDto()
            //{
            //    Id = product.Id,
            //    Description = product.Description,
            //    Name = product.Name,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    ProductBrand = product.ProductBrand.Name,
            //    ProductType = product.ProductType.Name
            //};

        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var brands = await _ProductTypeRepo.ListAllAsync();
            return Ok(brands);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var types = await _ProductBrandRepo.ListAllAsync();
            return Ok(types);

        }

    }
}
