using Domin.Models.Employees;
using Domin.Models.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models.Employees_Propeties
{
    [Table(name: "Employees_Properties", Schema = "Definition")]
    public class Employees_Properties : BaseEntity
    {
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        [ForeignKey(nameof(Property))]
        public int PropertyId { get; set; }
        public virtual Property? Property { get; set; }

        public string? Value { get; set; }

    }
}
