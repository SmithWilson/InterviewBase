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
    public class CustomerService : ICustomerService
    {
        private readonly MarketplaceContext _context;

        public CustomerService(MarketplaceContext context)
        {
            _context = context;
        }

        public async Task Add(Customer customer)
        {
            _context.Customers.Add(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Add Failed : {ex.Message}");
            }
        }

        public async Task<List<Customer>> Get(int count = 10, int offset = 0)
        {
            var customers = await _context.Customers
                .OrderBy(c => c.Id)
                .Skip(offset)
                .Take(count)
                .ToListAsync();

            return customers;
        }

        public async Task<List<Customer>> Get()
            => await _context.Customers.ToListAsync();

        public async Task<Customer> GetById(int id)
            => await _context.Customers
                    .Include(c => c.Orders)
                    .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<int> GetCount()
            => await _context.Customers.CountAsync();

        public async Task<bool> Remove(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Customer> Update(Customer newValue)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == newValue.Id);
            if (customer == null)
            {
                throw new ArgumentException(nameof(newValue));
            }

            var properties = newValue.GetType().GetProperties().Skip(1);
            foreach (var property in properties)
            {
                var value = property.GetValue(newValue);
                customer.GetType().GetProperty(property.Name).SetValue(customer, value);
            }

            try
            {
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Update Failed : {ex.Message}");
            }
        }
    }
}
