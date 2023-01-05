using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInvoicesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PurchaseInvoicesController(AppDBContext context)
        {
            _context = context;
        }


        // GET: api/PurchaseInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseInvoices>>> GetPurchaseInvoices()
        {
            return await _context.PurchaseInvoices.ToListAsync();
        }



        // GET: api/PurchaseInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseInvoices>> GetPurchaseInvoices(long id)
        {
            var purchaseInvoices = await _context.PurchaseInvoices.FindAsync(id);

            if (purchaseInvoices == null)
            {
                return NotFound();
            }

            return purchaseInvoices;
        }



        // PUT: api/PurchaseInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseInvoices(long id, PurchaseInvoices purchaseInvoices)
        {
            if (id != purchaseInvoices.Piid)
            {
                return BadRequest();
            }

            _context.Entry(purchaseInvoices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseInvoicesExists(id))
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



        // POST: api/PurchaseInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseInvoices>> PostPurchaseInvoices(PurchaseInvoices purchaseInvoices)
        {
            _context.PurchaseInvoices.Add(purchaseInvoices);
            await _context.SaveChangesAsync();  

            return CreatedAtAction("GetPurchaseInvoices", new { id = purchaseInvoices.Piid }, purchaseInvoices);
        }



        [HttpGet("GetProductIDwrtBarcode/{barcode}")] //get product Id from Product Prices table wrt barcode
        public async Task<IQueryable<Int64>> GetProductIDwrtBarcode(string barcode)
        {
            var x = _context.ProductPrices.Where(x => x.ProdPriceBarcode == barcode).Select(y => y.ProdPriceProductId);

            return x;
        }



        [HttpGet("GetDateFromProductPrices")] 
        public async Task<IQueryable<DateTime?>> GetDateFromProductPrices()
        {
            var x = _context.ProductPrices.Where(x => x.ProdPriceBuyingDate == DateTime.Today).Select(y => y.ProdPriceBuyingDate);

            return x;
        }



        [HttpGet("PutProductPricewrtDate")]
        public async Task<ActionResult>PutProductPricewrtDate(ProductPrices pp)
        {
            var x = await _context.GetProcedures().st_updateProductPriceswrtDateAsync(pp.ProdPriceProductId, pp.ProdPriceBuyingPrice);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
            }

            return NoContent();
        }



        // DELETE: api/PurchaseInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseInvoices(long id)
        {
            var purchaseInvoices = await _context.PurchaseInvoices.FindAsync(id);
            if (purchaseInvoices == null)
            {
                return NotFound();
            }

            _context.PurchaseInvoices.Remove(purchaseInvoices);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPut("PutStockPurchaseInvoice/{id}")]
        public async Task<IActionResult> PutStockPurchaseInvoice(long ProductId, int quantity)
        {
            var x = await _context.GetProcedures().st_updateStocksAsync(ProductId, quantity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }



        private bool PurchaseInvoicesExists(long id)
        {
            return _context.PurchaseInvoices.Any(e => e.Piid == id);
        }



        [HttpGet("GetProductwrtBarcode/{barcode}")]
        public async Task<List<st_getProductsbyBarcodeForPurchaseInvoicesResult>> GetProductwrtBarcode(string barcode)
        {
            return await _context.GetProcedures().st_getProductsbyBarcodeForPurchaseInvoicesAsync(barcode);
        }



        [HttpGet("GetProductQuantityFromPID/{id}")]
        public async Task<List<st_getProductQuantityResult>> GetProductQuantityFromPID(Int64 productID)
        {
            return await _context.GetProcedures().st_getProductQuantityAsync(productID);
        }



        [HttpGet("GetLastPurchaseInvoiceId")]
        public async Task<List<st_getLastPurchaseInvoiceIdResult>> GetLastPurchaseInvoiceId()
        {
            return await _context.GetProcedures().st_getLastPurchaseInvoiceIdAsync();
        }
    }
}
