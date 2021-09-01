using System;
using System.Collections.Generic;
using System.Text;
using Core;
using datalayer.Interfaces;
using datalayer.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using datalayer.Responses;
using datalayer.Requests;

namespace datalayer.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private ApplicationDataContext _context;

        public PersonRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<PersonResponse>> GetByIdAsync(int id)
        {
            return await _context.Persons
                .Select(p => new PersonResponse() { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Age = p.Age, Email = p.Email, Departments = p.Departments})
     
                /*.Include(s => s.Departments)*/
                .Where(p => p.Id == id)
                .ToArrayAsync();
        }

        public async Task<IReadOnlyList<PersonResponse>> GetByNameAsync(string name)
        {
                return await _context.Persons
                    .Select(p => new PersonResponse() { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Age = p.Age, Email = p.Email, Departments = p.Departments })
                    .Where(p => p.FirstName.Contains(string.IsNullOrEmpty(name) ? "" : name))
                    .ToArrayAsync();
        }

        public async Task<IReadOnlyList<PersonResponse>> GetWithPaginationAsync(int skip, int take, string search)
        {
            return await _context.Persons
                .Select(p => new PersonResponse() { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Age = p.Age, Email = p.Email, Departments = p.Departments })
                .Where(p => p.FirstName.Contains(string.IsNullOrEmpty(search) ? "" : search))
                .Skip(skip)
                .Take(take)
                .ToArrayAsync();
        }


        public async Task AddAsync(IReadOnlyList<PersonRequest> item)
        {
            foreach(var p in item.ToList())
            {
                await _context.Persons.AddAsync(new Person() { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Age = p.Age, Email = p.Email });
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(IReadOnlyList<PersonRequest> item)
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
