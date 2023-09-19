using System;
using Domain;

namespace Application.Repositories
{
	public interface IPropertyRepo
	{
        Task AddNewAsync(Property property);
        Task<int> DeleteAsync(Property property);
        Task<List<Property>> GetAllAsync();
        Task UpdateAsync(Property property);
        Task<Property> GetByIdAsync(int id);
    }
}

