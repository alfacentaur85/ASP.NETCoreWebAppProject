using System;
using System.Collections.Generic;
using System.Text;
using Core;
using datalayer.Interfaces;
using datalayer.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace datalayer.Repositories
{
    public class PersonDepartmentsRepository : IPersonDepartmentsRepository
    {
        private ApplicationDataContext _context;

        public PersonDepartmentsRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<PersonDepartment>> GetByIdAsync(int id)
        {
            return await _context.PersonDepartments
                .Where(p => p.Id == id).Include(p => p.Person).Include(g => g.Department)
                .ToListAsync();
        }

     
        public async Task<IReadOnlyList<PersonDepartment>> GetWithPaginationAsync(int skip, int take, string search)
        {
            return await _context.PersonDepartments.Where(p => p.Id == 1).Skip(skip).Take(take).Include(p => p.Person).Include(g => g.Department).ToArrayAsync();
        }


        public async Task AddAsync(IReadOnlyList<PersonDepartment> item)
        {
            foreach(var p in item.ToList())
            {
                await _context.PersonDepartments.AddAsync(p);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(IReadOnlyList<PersonDepartment> item)
        {
            foreach (var p in item.ToList())
            {

                var entity = await _context
                               .PersonDepartments
                               .Where(entity => entity.Id == p.Id)
                               .FirstOrDefaultAsync();
                if (entity != null)
                {
                    
                    entity.Id = p.Id;
                    
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context
                .PersonDepartments
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
