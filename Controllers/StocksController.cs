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
    public class StocksController : ControllerBase
    {
        private readonly AppDBContext _context;

        public StocksController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stocks>>> GetStocks()
        {
            return await _context.Stocks.ToListAsync();
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stocks>> GetStocks(long id)
        {
            var stocks = await _context.Stocks.FindAsync(id);

            if (stocks == null)
            {
                return NotFound();
            }

            return stocks;
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStocks(long id, Stocks stocks)
        {
            //if (id != stocks.StockProductId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(stocks).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!StocksExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();

            var x = await _context.GetProcedures().st_updateStocksAsync(id, stocks.StockQuantity);

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

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stocks>> PostStocks(Stocks stocks)
        {
            _context.Stocks.Add(stocks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStocks", new { id = stocks.StockId }, stocks);
        }

        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStocks(long id)
        {
            var stocks = await _context.Stocks.FindAsync(id);
            if (stocks == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stocks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StocksExists(long id)
        {
            return _context.Stocks.Any(e => e.StockProductId == id);
        }
    }
}
