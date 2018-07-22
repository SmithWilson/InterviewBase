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
    public class ProductService : IProductService
    {
        private readonly MarketplaceContext _context;

        public ProductService(MarketplaceContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Add Failed : {ex.Message}");
            }
        }

        public async Task<List<Product>> Get(int count, int offset)
        {
            var products = await _context.Products
                .Include(p => p.Type)
                .OrderBy(p => p.Id)
                .Skip(offset)
                .Take(count)
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> Get()
            => await _context.Products.ToListAsync();

        public async Task<Product> GetById(int id)
            =>  await _context.Products
                    .Include(p => p.Orders)
                    .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<int> GetCount()
            => await _context.Products.CountAsync();

        public async Task<bool> Remove(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
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

        public async Task<Product> Update(Product newValue)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == newValue.Id);
            if (product == null)
            {
                throw new ArgumentException(nameof(newValue));
            }

            var properties = newValue.GetType().GetProperties().Skip(1);
            foreach (var property in properties)
            {
                var value = property.GetValue(newValue);
                product.GetType().GetProperty(property.Name).SetValue(product, value);
            }

            try
            {
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Update Failed : {ex.Message}");
            }
        }
    }
}
