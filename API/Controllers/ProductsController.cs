using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController 
    {
        public IGenericRepository<Product> _productRepo;

        public IGenericRepository<ProductBrand> _productBrandRepo;

        public IGenericRepository<ProductType> _productTypeRepo;

        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _productRepo = productRepo;

            _productBrandRepo = productBrandRepo;

            _productTypeRepo = productTypeRepo;

            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(); // spec contains the include statements

            var products = await _productRepo.ListAsync(spec);

            return _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products).ToList();
        }

        [HttpGet("{id}")]      // {} is used if want to pass a parameter to the api method
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id); //creates new instance // we hit the contructor with para

            var product = await _productRepo.GetEntityWithSpec(spec); // findasync will search and fetch the primary key

            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}
