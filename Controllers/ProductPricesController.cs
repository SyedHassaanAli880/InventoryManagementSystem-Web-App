using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPricesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProductPricesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrices>>> GetProductPrices()
        {
            return await _context.ProductPrices.ToListAsync();
        }



        // GET: api/ProductPrices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPrices>> GetProductPrices(long id)
        {
            var productPrices = await _context.ProductPrices.FindAsync(id);

            if (productPrices == null)
            {
                return NotFound();
            }

            return productPrices;
        }



        // PUT: api/ProductPrices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductPrices(long id, ProductPrices productPrices)
        {
            if (id != productPrices.ProdPriceProductId)
            {
                return BadRequest();
            }

            _context.Entry(productPrices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(productPrices);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPricesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //await _context.GetProcedures().st_updateProductPricesAsync(productPrices.ProdPriceSellingPrice,productPrices.ProdPriceProfit,productPrices.ProdPriceDiscount, Convert.ToInt64(productPrices.ProdPriceBuyingPrice),id);

            //return NoContent();
        }

            

        // POST: api/ProductPrices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductPrices>> PostProductPrices(ProductPrices productPrices)
        {
            _context.ProductPrices.Add(productPrices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductPrices", new { id = productPrices.ProdPriceId }, productPrices);
        }

            

        // DELETE: api/ProductPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductPrices(long id)
        {
            var productPrices = await _context.ProductPrices.FindAsync(id);
            if (productPrices == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(productPrices);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool ProductPricesExists(long id)
        {
            return _context.ProductPrices.Any(e => e.ProdPriceId == id);
        }



        [HttpGet("GetCategoriesList")]
        public async Task<List<st_getCategoriesListResult>> GetCategoriesList()
        {
            var catList = await _context.GetProcedures().st_getCategoriesListAsync();

            return catList;
        }

        [HttpGet("GetProductwrtCategories/{id}")]
        public async Task<List<st_getProductswrtCategoriesResult>> GetProductwrtCategories(long id)
        {
            var PwrtC = await _context.GetProcedures().st_getProductswrtCategoriesAsync(Convert.ToInt32(id));

            return PwrtC;
        }

    }
}
