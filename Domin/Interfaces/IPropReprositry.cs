using Domin.Models.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Interfaces
{
    public interface IPropReprositry
    {
        Task<List<Property>> GetAll();
        Task<Property> GetById(int id);

        Task Add(Property property);
        Task Update(Property property);
        Task Remove(Property property);
        Task<bool> IsPropertyInUse(int propertyId);
    }
}
