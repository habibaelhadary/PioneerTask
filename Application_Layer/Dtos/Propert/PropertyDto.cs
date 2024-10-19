using Application_Layer.Dtos.DropDown;
using Domin.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Dtos.Propert
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PropertyTypes Type { get; set; }
        public bool Required { get; set; }
        public List<DropDownDto>? DropDownValues { get; set; } = new List<DropDownDto>();

    }
}
