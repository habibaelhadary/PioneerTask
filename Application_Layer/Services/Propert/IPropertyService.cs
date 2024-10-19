using Application_Layer.Dtos.Propert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Services.Propert
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetAllProperty();
        Task<PropertyDto> GetByIdProperty(int id);
        Task AddProperty(PropertyDto propertyDto);
        Task UpdateProperty(PropertyDto propertyDto);
        Task Remove(int id);
        Task<bool> IsProperty(int propertyId);
    }
}
