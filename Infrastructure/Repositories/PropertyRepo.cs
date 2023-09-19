using System;
using Application;
using Application.Repositories;
using Domain;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyRepo : IPropertyRepo
	{

        private readonly ApplicationDbContext _context;

        public PropertyRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNewAsync(Property property)
        {
            await _context.properties.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Property property)
        {
            var deleted = await _context.properties.Where(p => p.Id == property.Id).ExecuteDeleteAsync();
            return deleted;
        }

        public async Task<List<Property>> GetAllAsync()
        {
            var properties = await _context.properties.Include(p=> p.Images).ToListAsync();
            return properties;
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            // var property = await _context.properties.FindAsync(id);
            var property = await _context.properties
                                            .Where(p => p.Id == id)
                                            .Include(p=> p.Images)
                                            .FirstOrDefaultAsync();
            return property;
        }

        public async Task UpdateAsync(Property property)
        {
             _context.properties.Update(property);

            await _context.SaveChangesAsync();

        }
    }
}

