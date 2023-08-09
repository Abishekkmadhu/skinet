using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController] // this is api controller attribute used to define all the controllers in this is an api
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase // return https endpoints so use this without view
    {
        private readonly StoreContext _context;  // to access the db context

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()   
        {
            return await _context.Products.ToListAsync(); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id); // findasync will search and fetch the primary key
        }
    }
}
