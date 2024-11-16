using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTwo.Data;
using TaskTwo.DTOs;
using TaskTwo.Model;

namespace TaskTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost("Addproduct")]
        public async Task<IActionResult> AddProduct(AddOrEditProductDto product)
        {
            if (ModelState.IsValid)
            {
                var pro = product.Adapt<Product>();
                await context.Products.AddAsync(pro);
                await context.SaveChangesAsync();
                return Created();

            }
            return BadRequest();
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is not null)
            {
                var pro = product.Adapt<GetProductDto>();
                return Ok(product);
            }
            return NotFound();
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await context.Products.ToListAsync();
            if (products is not null)
            {
                var pro = products.Adapt<IEnumerable<GetProductDto>>();
                return Ok(pro);
            }

            return BadRequest();
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, AddOrEditProductDto product)
        {
            if (ModelState.IsValid)
            {
                var pro = await context.Products.FindAsync(id);
                if (pro is not null)
                {
                    pro.Name = product.Name;
                    pro.Description = product.Description;
                    pro.Price = product.Price;
                    await context.SaveChangesAsync();
                    return Created();
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> actionResult(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product is not null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();

        }
    }
}
