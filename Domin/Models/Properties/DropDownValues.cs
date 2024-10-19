using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Models.Properties
{
    [Table(name: "DropDownValues", Schema = "Definition")]
    public class DropDownValues : BaseEntity
    {

        [ForeignKey(nameof(Property))]
        public int PropertyId { get; set; }
        public Property? Property { get; set; }
        public string Value { get; set; }
    }
}
