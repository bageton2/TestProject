using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.Product)
                .Include(o => o.Status)
                .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.Product)
                .Include(o => o.Status)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            try
            {
                order.StatusId = GetStatus(order.Status).Id;
                order.ProductId = GetProduct(order.Product).Id;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            Product product = null;
            Status status = null;
            try
            {
                 status = GetStatus(order.Status);
                 product = GetProduct(order.Product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _context.Orders.Add(new Order()
            {
                ProductId = product.Id,
                StatusId = status.Id,
                Count = order.Count
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }


        private Product GetProduct(Product product)
        {
            Product result = _context.Products.FirstOrDefault(p => p.Name.ToLower() == product.Name.ToLower()
                                                                      && p.PhotoUrl == product.PhotoUrl
                                                                      && Decimal.Equals(p.Price, product.Price));

            if (result == null)
            {
                throw new Exception("Product not found!");
            }

            return result;
        }

        private Status GetStatus(Status status)
        {
            Status result = _context.Statuses.FirstOrDefault(s => s.Name == status.Name);

            if (result == null)
            {
                throw new Exception("Status not found!");
            }

            return result;
        }
    }
}
