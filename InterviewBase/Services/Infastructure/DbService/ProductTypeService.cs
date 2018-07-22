using InterviewBase.Models;
using InterviewBase.Models.Entities;
using InterviewBase.Services.Abstractions.DbSevice;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InterviewBase.Services.Infastructure.DbService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly MarketplaceContext _context;

        public ProductTypeService(MarketplaceContext context)
        {
            _context = context;
        }

        public async Task Add(ProductType productType)
        {
            _context.ProductTypes.Add(productType);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException($"Add Failed : {productType}");
            }
        }

        public async Task<List<ProductType>> Get()
            => await _context.ProductTypes.ToListAsync();

        public async Task<ProductType> GetById(int id)
            => await _context.ProductTypes
            .Include(pt => pt.Products)
            .FirstOrDefaultAsync(pt => pt.Id == id);

        public async Task<bool> Remove(int id)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
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

        public async Task<ProductType> Update(ProductType newValue)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == newValue.Id);
            if (productType == null)
            {
                throw new ArgumentException(nameof(newValue));
            }

            productType.Name = newValue.Name;

            try
            {
                await _context.SaveChangesAsync();
                return productType;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Update Failed : {ex.Message}");
            }
        }
    }
}
