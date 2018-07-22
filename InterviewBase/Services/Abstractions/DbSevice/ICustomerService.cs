using InterviewBase.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewBase.Services.Abstractions.DbSevice
{
    public interface ICustomerService
    {
        Task<Customer> GetById(int id);

        Task<List<Customer>> Get(int count, int offset);

        Task Add(Customer customer);

        Task<bool> Remove(int id);

        Task<Customer> Update(Customer newValue);

        Task<int> GetCount();
    }
}
