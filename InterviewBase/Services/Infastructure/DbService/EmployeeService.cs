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
    public class EmployeeService : IEmployeeService
    {
        private readonly MarketplaceContext _context;

        public EmployeeService(MarketplaceContext context)
        {
            _context = context;
        }

        public async Task Add(Employee employee)
        {
            _context.Employees.Add(employee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException($"The employee already exists : {employee}");
            }
        }

        public async Task<List<Employee>> Get(int count, int offset)
        {
            var employees = await _context.Employees
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(count)
                .ToListAsync();

            return employees;
        }

        public async Task<List<Employee>> Get()
            => await _context.Employees.ToListAsync();


        public async Task<Employee> GetById(int id)
            => await _context.Employees
                    .Include(e => e.Orders)
                    .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<int> GetCount()
            => await _context.Employees.CountAsync();

        public async Task<bool> Remove(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
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

        public async Task<Employee> Update(Employee newValue)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == newValue.Id);
            if (employee == null)
            {
                throw new ArgumentException(nameof(newValue));
            }

            var properties = newValue.GetType().GetProperties().Skip(1);
            foreach (var property in properties)
            {
                var value = property.GetValue(newValue);
                employee.GetType().GetProperty(property.Name).SetValue(employee, value);
            }

            try
            {
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Update Failed : {ex.Message}");
            }
        }
    }
}
