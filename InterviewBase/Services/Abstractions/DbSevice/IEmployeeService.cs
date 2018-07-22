using InterviewBase.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewBase.Services.Abstractions.DbSevice
{
    public interface IEmployeeService
    {
        Task<Employee> GetById(int id);

        Task<List<Employee>> Get(int count, int offset);

        Task Add(Employee employee);

        Task<bool> Remove(int id);

        Task<Employee> Update(Employee newValue);

        Task<int> GetCount();
        Task<List<Employee>> Get();
    }
}
