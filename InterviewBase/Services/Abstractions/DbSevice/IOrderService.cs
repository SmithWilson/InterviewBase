using InterviewBase.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewBase.Services.Abstractions.DbSevice
{
    public interface IOrderService
    {
        Task<Order> GetById(int id);

        Task<List<Order>> Get(int count, int offset);
        
        Task<List<Order>> GetByPrice(int count, int offset);

        Task<List<Order>> GetByCustomer(string email);

        Task Add(Order order);

    }
}
