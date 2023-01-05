using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInvoiceDetailsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PurchaseInvoiceDetailsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseInvoiceDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseInvoiceDetails>>> GetPurchaseInvoiceDetails()
        {
            return await _context.PurchaseInvoiceDetails.ToListAsync();
        }

        // GET: api/PurchaseInvoiceDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseInvoiceDetails>> GetPurchaseInvoiceDetails(long id)
        {
            var purchaseInvoiceDetails = await _context.PurchaseInvoiceDetails.FindAsync(id);

            if (purchaseInvoiceDetails == null)
            {
                return NotFound();
            }

            return purchaseInvoiceDetails;
        }

        // PUT: api/PurchaseInvoiceDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseInvoiceDetails(long id, PurchaseInvoiceDetails purchaseInvoiceDetails)
        {
            if (id != purchaseInvoiceDetails.Pidid)
            {
                return BadRequest();
            }

            _context.Entry(purchaseInvoiceDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseInvoiceDetailsExists(id))
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

        // POST: api/PurchaseInvoiceDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseInvoiceDetails>> PostPurchaseInvoiceDetails(PurchaseInvoiceDetails purchaseInvoiceDetails)
        {
            _context.PurchaseInvoiceDetails.Add(purchaseInvoiceDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseInvoiceDetails", new { id = purchaseInvoiceDetails.Pidid }, purchaseInvoiceDetails);
        }

        // DELETE: api/PurchaseInvoiceDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseInvoiceDetails(long id)
        {
            var purchaseInvoiceDetails = await _context.PurchaseInvoiceDetails.FindAsync(id);
            if (purchaseInvoiceDetails == null)
            {
                return NotFound();
            }

            _context.PurchaseInvoiceDetails.Remove(purchaseInvoiceDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseInvoiceDetailsExists(long id)
        {
            return _context.PurchaseInvoiceDetails.Any(e => e.Pidid == id);
        }
    }
}
