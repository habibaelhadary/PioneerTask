using Application_Layer.Dtos.Propert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Dtos.Employee
{
    public class GetEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public List<PropertyDto>? Properties { get; set; } = new List<PropertyDto>();
        public List<EmployeePropertiesDto>? SelectedProperties { get; set; } = new List<EmployeePropertiesDto>();


    }
}
