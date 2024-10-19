using Domin.Interfaces;
using Domin.Models.Properties;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.PropertyRepositor
{
    public class ProRepository : IPropReprositry
    {
        private readonly PioneerManagementDbContext _context;

        public ProRepository(PioneerManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<Property>> GetAll()
        {
            return await _context.Properties.Include(x => x.DropDownValues).Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<Property> GetById(int id)
        {
            return _context.Properties.Include(x => x.DropDownValues)
                .FirstOrDefault(p => p.Id == id);
        }

        public async Task Add(Property property)
        {
            try
            {
                var NameExist = _context.Properties.Where(x => x.Name == property.Name).FirstOrDefault();
                if (NameExist == null)
                {
                    await _context.Properties.AddAsync(property);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(" adding the property before", ex);
            }
        }

        public async Task Update(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }
        public async Task Remove(Property property)
        {
            var propertyGet = _context.Properties.Include(x => x.DropDownValues).FirstOrDefault(x => x.Id == property.Id);
            //soft delete
            if (propertyGet != null)
            {
                propertyGet.IsDeleted = true;

                //_context.Properties.Update(propertyGet);
                //await _context.SaveChangesAsync();
                Update(propertyGet);

            }

        }

    }
}
