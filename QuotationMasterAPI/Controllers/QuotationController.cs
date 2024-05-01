using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using QuotationMasterAPI.Data;
using QuotationMasterAPI.Models;
using System.Runtime.InteropServices;

namespace QuotationMasterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 
 
    public QuotationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Order 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationMaster>>> GetOrders()
        {
            return await _context.QuotationMasters.Include(o => o.QuotationDetails).ToListAsync();
        }

        // GET: api/Order/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationMaster>> GetQuotationMaster(int id)
        {
            var quotationMaster = await _context.QuotationMasters.Include(o => o.QuotationDetails).FirstOrDefaultAsync(o => o.QId == id);

            if (quotationMaster == null)
            {
                return NotFound();
            }

            return quotationMaster;
        }

        // POST: api/Order 
        [HttpPost]
        public async Task<ActionResult<QuotationMaster>> PostQuotationMaster(QuotationMaster quotationMaster)
        {
            _context.QuotationMasters.Add(quotationMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuotationMaster", new { id = quotationMaster.QId }, quotationMaster);
        }

        //// PUT: api/Order/5 
        //[H pPut("{id}")]
        //public async Task<IAc onResult> PutOrder(int id, Order order)
        //{
        //    if (id != order.OrderId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(order).State = En tyState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyExcep on)
        //    {
        //        if (!_context.Orders.Any(e => e.OrderId == id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/Order/5 
        //[H pDelete("{id}")]
        //public async Task<IAc onResult> DeleteOrder(int id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
