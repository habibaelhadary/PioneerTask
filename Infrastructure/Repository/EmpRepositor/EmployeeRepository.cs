using Domin.Interfaces;
using Domin.Models.Employees;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.EmpRepositor
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PioneerManagementDbContext _context;

        public EmployeeRepository(PioneerManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> getAll()
        {
            return await _context.Employees.Include(x => x.EmployeeProperties).ThenInclude(ep => ep.Property).Where(e => e.IsDeleted == false).ToListAsync();
        }

        public async Task<Employee> getById(int id)
        {
            return _context.Employees.Include(x => x.EmployeeProperties).ThenInclude(ep => ep.Property).ThenInclude(d => d.DropDownValues)
                .FirstOrDefault(e => e.Id == id && e.IsDeleted==false);
        }
        public async Task Add(Employee employee)
        {
            try
            {
                var CodeExist = _context.Employees.Where(x => x.Code == employee.Code && x.IsDeleted == false).FirstOrDefault();
                if (CodeExist == null)
                {
                    await _context.Employees.AddAsync(employee);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("adding the Employee before", ex);
            }

        }

        public async Task Update(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
        public async Task Remove(int id)
        {
            var foundEmp = await getById(id);
            if (foundEmp != null)
            {
                foundEmp.IsDeleted = true;
                if (foundEmp.EmployeeProperties != null)
                {
                    foreach (var emp in foundEmp.EmployeeProperties)
                    {
                        emp.IsDeleted = true;
                    }
                }
             await   Update(foundEmp);
            }
        }
    }
}
