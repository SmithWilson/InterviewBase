using InterviewBase.Models;
using InterviewBase.Models.Entities;
using InterviewBase.Services.Abstractions.DbSevice;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewBase.Services.Infastructure.DbService
{
    public class OrderService : IOrderService
    {
        private readonly MarketplaceContext _context;

        public OrderService(MarketplaceContext context)
        {
            _context = context;
        }

        public async Task Add(Order order)
        {
            _context.Orders.Add(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Add Failed : {ex.Message}");
            }
        }

        public async Task<List<Order>> Get(int count, int offset)
        {
            var orders = await _context.Orders
                .Skip(offset)
                .Take(count)
                .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetByCustomer(string email)
        {
            var orders = await _context.Orders
                .Where(o => o.Customer.Email.Contains(email))
                .ToListAsync();

            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order;
        }

        public async Task<List<Order>> GetByPrice(int count, int offset)
        {
            var orders = await _context.Orders
                .OrderByDescending(o => o.Price)
                .Skip(offset)
                .Take(count)
                .ToListAsync();

            return orders;
        }
    }
}
