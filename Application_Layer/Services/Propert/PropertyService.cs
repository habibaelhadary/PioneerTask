using Application_Layer.Dtos.DropDown;
using Application_Layer.Dtos.Propert;
using Domin.Interfaces;
using Domin.Models.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Services.Propert
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropReprositry _propReprositry;
        public PropertyService(IPropReprositry propReprositry)
        {
            _propReprositry = propReprositry;
        }
        public async Task<List<PropertyDto>> GetAllProperty()
        {
            var PropList = await _propReprositry.GetAll();
            return PropList.Select(p => new PropertyDto
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                Required = p.Required,
                DropDownValues = p.DropDownValues?.Select(dv => new DropDownDto
                {
                    Value = dv.Value
                }).ToList()
            }
            ).ToList();
        }
        public async Task<PropertyDto> GetByIdProperty(int id)
        {
            var PropertyById = await _propReprositry.GetById(id);
            if (PropertyById == null)
                return null;
            return new PropertyDto
            {
                Id = PropertyById.Id,
                Name = PropertyById.Name,
                Type = PropertyById.Type,
                Required = PropertyById.Required,
                DropDownValues = PropertyById.DropDownValues?.Select(dv => new DropDownDto
                {
                    Value = dv.Value
                }).ToList()
            };
        }

        public async Task AddProperty(PropertyDto propertyDto)
        {
            var propertyNew = new Domin.Models.Properties.Property
            {
                Name = propertyDto.Name,
                Type = propertyDto.Type,
                Required = propertyDto.Required,
                DropDownValues = propertyDto.DropDownValues?.Select(dv => new DropDownValues
                { Value = dv.Value }).ToList()
            };
            _propReprositry.Add(propertyNew);
        }

        public async Task UpdateProperty(PropertyDto propertyDto)
        {
            var propId = await _propReprositry.GetById(propertyDto.Id);
            propId.Name = propertyDto.Name;
            propId.Type = propertyDto.Type;
            propId.Required = propertyDto.Required;

            // Update dropdown values
            propId.DropDownValues = propertyDto.DropDownValues.Select(v => new DropDownValues
            {
                Id = v.Id,
                Value = v.Value
            }).ToList();

            await _propReprositry.Update(propId);


        }

        public async Task Remove(PropertyDto propertyDto)
        {
            var property = await _propReprositry.GetById(propertyDto.Id);

            if (property != null)
            {
                await _propReprositry.Remove(property);
            }
        }



    }
}
