using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Models;

namespace Web_Api_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext context;

        public ProductsController(DataContext context)
        {
            this.context = context;
        }

        //Get api/products
        [Produces("application/json")]
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products =await context.Products.ToListAsync();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }
        //Get api/products/5
        [Produces("application/json")]
        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product =await context.Products.FindAsync(id);
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
        //Post api/products
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> PostProduct(Product product)
        {
            try
            {
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Products/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProducts(int id)
        {
            var products = await context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            context.Products.Remove(products);
            await context.SaveChangesAsync();

            return products;
        }

        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.Id == id);
        }
    }
}