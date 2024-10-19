using Application_Layer.Dtos.DropDown;
using Application_Layer.Dtos.Employee;
using Application_Layer.Dtos.Propert;
using Domin.Interfaces;
using Domin.Models.Employees;
using Domin.Models.Employees_Propeties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Services.EmpServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPropReprositry _propReprositry;

        public EmployeeService(IEmployeeRepository employeeRepository, IPropReprositry propReprositry)
        {
            _employeeRepository = employeeRepository;
            _propReprositry = propReprositry;

        }


        public async Task AddEmployee(GetEmployeeDto employeeDto)
        {
            var emp = new Employee
            {
                Code = employeeDto.EmployeeCode,
                Name = employeeDto.EmployeeName,
                EmployeeProperties = new List<Employees_Properties>()
            };

            foreach (var prop in employeeDto.SelectedProperties)
            {
                var property = await _propReprositry.GetById(prop.PropId);

                if (property != null)
                {

                    var employeeProperty = new Employees_Properties
                    {
                        PropertyId = property.Id,
                        Value = prop.Value,

                    };

                    emp.EmployeeProperties.Add(employeeProperty);
                }
            }


            await _employeeRepository.Add(emp);
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployee()
        {
            var EmployeeList = await _employeeRepository.getAll();
            return EmployeeList.Select(
                p => new GetEmployeeDto
                {
                    EmployeeId = p.Id,
                    EmployeeCode = p.Code,
                    EmployeeName = p.Name,
                    Properties = p.EmployeeProperties.Select(emp =>
                    new Dtos.Propert.PropertyDto
                    {
                        Id = emp.PropertyId,
                        Name = emp.Property.Name,
                        Type = emp.Property.Type,
                        Required = emp.Property.Required,

                    }).ToList(),
                    SelectedProperties = p.EmployeeProperties.Select(
                        ep => new EmployeePropertiesDto
                        {
                            PropId = ep.PropertyId,
                            Value = ep.Value,
                        }
                        ).ToList(),

                }

                ).ToList();
        }

        public async Task<GetEmployeeDto> GetByIdEmployee(int id)
        {
            var EmpId = await _employeeRepository.getById(id);
            if (EmpId == null)
                return null;
            return new GetEmployeeDto
            {
                EmployeeId = EmpId.Id,
                EmployeeCode = EmpId.Code,
                EmployeeName = EmpId.Name,
                Properties = EmpId.EmployeeProperties.Select(emp =>
                new PropertyDto
                {
                    Id = emp.PropertyId,
                    Name = emp.Property.Name,
                    Type = emp.Property.Type,
                    Required = emp.Property.Required,
                    DropDownValues = emp.Property.DropDownValues.Select(x => new DropDownDto
                    {
                        Id = x.PropertyId,
                        Value = x.Value
                    }).ToList()
                }).ToList(),
                SelectedProperties = EmpId.EmployeeProperties.Select(
                        ep => new EmployeePropertiesDto
                        {
                            PropId = ep.PropertyId,
                            Value = ep.Value,
                        }
                        ).ToList()
            };
        }
        public async Task UpdateEmployee(GetEmployeeDto emp)
        {

            var Emp = await _employeeRepository.getById(emp.EmployeeId);
            //Emp.EmployeeId = emp.EmployeeId;
            Emp.Code = emp.EmployeeCode;

            Emp.Name = emp.EmployeeName;
            Emp.EmployeeProperties.Clear();
            foreach (var selectedProperty in emp.SelectedProperties)
            {

                var employeeProperty = new Employees_Properties
                {
                    EmployeeId = Emp.Id,
                    PropertyId = selectedProperty.PropId,
                    Value = selectedProperty.Value
                };

                Emp.EmployeeProperties.Add(employeeProperty);
            }
            _employeeRepository.Update(Emp);
        }


        public async Task DeleteEmployee(GetEmployeeDto emp)
        {


            var employee = await _employeeRepository.getById(emp.EmployeeId);

            if (employee != null)
            {
                await _employeeRepository.Remove(emp.EmployeeId);
            }
        }

    }
}
