using Application_Layer.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Services.EmpServices
{
    public interface IEmployeeService
    {
        Task AddEmployee(GetEmployeeDto employeeDto);
        Task<List<GetEmployeeDto>> GetAllEmployee();
        Task UpdateEmployee(GetEmployeeDto emp);
        Task<GetEmployeeDto> GetByIdEmployee(int id);
        Task DeleteEmployee(GetEmployeeDto emp);
    }
}
