using InterviewBase.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewBase.Services.Abstractions.DbSevice
{
    public interface IProductTypeService
    {
        Task<ProductType> GetById(int id);

        Task<List<ProductType>> Get();

        Task Add(ProductType productType);

        Task<bool> Remove(int id);

        Task<ProductType> Update(ProductType newValue);
    }
}
