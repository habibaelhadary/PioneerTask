using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Models.Employees_Propeties;

namespace Domin.Models.Employees
{
    [Table(name: "Employees", Schema = "Definition")]
    public class Employee : BaseEntity
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public virtual ICollection<Employees_Properties>? EmployeeProperties { get; set; }
    }
}
