using System.Collections.Generic;
using datalayer.Interfaces;
using datalayer.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using datalayer.Responses;
using datalayer.Requests; 


namespace datalayer.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private ApplicationDataContext _context;

        public DepartmentRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<DepartmentResponse>> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Select(p => new DepartmentResponse{Id = p.Id, Name = p.Name, Persons = p.Persons})
                .Where(p => p.Id == id)
                .ToArrayAsync();
        }

        public async Task<IReadOnlyList<DepartmentResponse>> GetByNameAsync(string name)
        {
                return await _context.Departments
                    .Select(p => new DepartmentResponse { Id = p.Id, Name = p.Name, Persons = p.Persons })
                    .Where(p => p.Name.Contains(string.IsNullOrEmpty(name) ? "" : name))
                    .ToArrayAsync();
        }

        public async Task<IReadOnlyList<DepartmentResponse>> GetWithPaginationAsync(int skip, int take, string search)
        {
            return await _context.Departments
                .Select(p => new DepartmentResponse { Id = p.Id, Name = p.Name, Persons = p.Persons })
                .Where(p => p.Name.Contains(string.IsNullOrEmpty(search) ? "" : search))
                .Skip(skip)
                .Take(take)
                .ToArrayAsync();
        }


        public async Task AddAsync(IReadOnlyList<DepartmentRequest> item)
        {
            foreach(var p in item.ToList())
            {
                await _context.Departments.AddAsync(new Department() { Id = p.Id, Name = p.Name});
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(IReadOnlyList<DepartmentRequest> item)
        {
            foreach (var p in item.ToList())
            {

                var entity = await _context
                               .Departments
                               .Where(entity => entity.Id == p.Id)
                               .FirstOrDefaultAsync();
                if (entity != null)
                {
                    
                    entity.Name = p.Name;
                    
                }
            }
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context
                .Persons
                .Where(entity => entity.Id == id)
                .FirstOrDefaultAsync();
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            } 
        }
    }
}
