using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> brandsRepo,
        IGenericRepository<ProductType> typesRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;    
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCount(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var products =  await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<List<Product>, List<ProductDto>>(products);
            
             return Ok(new Pagination<ProductDto>(productParams.PageIndex,productParams.PageSize,totalItems, data));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands =  await _brandsRepo.GetAllAsync();
            return  Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes =  await _typesRepo.GetAllAsync();
            return  Ok(productTypes);
        }
        
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductDto>(product);
        }
        
    }
}