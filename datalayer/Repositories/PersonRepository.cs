using System;
using System.Collections.Generic;
using System.Text;
using Core;
using datalayer.Interfaces;
using datalayer.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace datalayer.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private ApplicationDataContext _context;

        public PersonRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetByIdAsync(int id)
        {
            return await _context.Persons
                .Where(p => p.Id == id)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Person>> GetByNameAsync(string name)
        {
                return await _context.Persons
                    .Where(p => p.FirstName.Contains(name))
                    .ToArrayAsync();
        }

        public async Task<IEnumerable<Person>> GetWithPaginationAsync(int skip, int take, string search)
        {
            return await _context.Persons.Where(p => p.FirstName.Contains(search)).Skip(skip).Take(take).ToArrayAsync();
        }


        public async Task AddAsync(IEnumerable<Person> item)
        {
            foreach(var p in item.ToList())
            {
                await _context.Persons.AddAsync(p);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(IEnumerable<Person> item)
        {
            foreach (var p in item.ToList())
            {

                var entity = await _context
                               .Persons
                               .Where(entity => entity.Id == p.Id)
                               .FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Age = p.Age;
                    entity.Company = p.Company;
                    entity.Email = p.Email;
                    entity.FirstName = p.FirstName;
                    entity.LastName = p.LastName;
                    await _context.SaveChangesAsync();
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
