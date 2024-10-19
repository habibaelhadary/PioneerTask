using Domin.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> getAll();
        Task<Employee> getById(int id);
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Remove(int id);
    }
}
