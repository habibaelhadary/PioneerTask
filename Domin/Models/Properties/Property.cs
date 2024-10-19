using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Enum;
using Domin.Models.Employees_Propeties;

namespace Domin.Models.Properties
{
    [Table(name: "Properties", Schema = "Definition")]
    public class Property : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public PropertyTypes Type { get; set; }

        public bool Required { get; set; }
        public virtual ICollection<Employees_Properties>? EmployeeProperties { get; set; }
        public virtual ICollection<DropDownValues>? DropDownValues { get; set; }
    }
}
