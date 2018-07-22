using InterviewBase.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewBase.Services.Abstractions.DbSevice
{
    public interface IProductService
    {
        Task<Product> GetById(int id);

        Task<List<Product>> Get(int count, int offset);

        Task Add(Product product);

        Task<bool> Remove(int id);

        Task<Product> Update(Product newValue);
    }
}
