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
    public class SaleDetailsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SaleDetailsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/SaleDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDetails>>> GetSaleDetails()
        {
            return await _context.SaleDetails.ToListAsync();
        }

        // GET: api/SaleDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDetails>> GetSaleDetails(long id)
        {
            var saleDetails = await _context.SaleDetails.FindAsync(id);

            if (saleDetails == null)
            {
                return NotFound();
            }

            return saleDetails;
        }

        // PUT: api/SaleDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleDetails(long id, SaleDetails saleDetails)
        {
            if (id != saleDetails.SalDetId)
            {
                return BadRequest();
            }

            _context.Entry(saleDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleDetailsExists(id))
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

        // POST: api/SaleDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaleDetails>> PostSaleDetails(SaleDetails saleDetails)
        {
            _context.SaleDetails.Add(saleDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleDetails", new { id = saleDetails.SalDetId }, saleDetails);
        }

        // DELETE: api/SaleDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleDetails(long id)
        {
            var saleDetails = await _context.SaleDetails.FindAsync(id);
            if (saleDetails == null)
            {
                return NotFound();
            }

            _context.SaleDetails.Remove(saleDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleDetailsExists(long id)
        {
            return _context.SaleDetails.Any(e => e.SalDetId == id);
        }
    }
}
