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
    public class PersonDepartmentRepository : IPersonDepartmentRepository
    {
        private ApplicationDataContext _context;

        public PersonDepartmentRepository(ApplicationDataContext context)
        {
            _context = context;
        }

      

        public async Task AddAsync(IReadOnlyList<PersonDepartmentRequest> item)
        {
            foreach(var p in item.ToList())
            {
                await _context.PersonsDepartments.AddAsync(new PersonDepartment {Id = p.Id, DepartmentId = p.DepartmentId, PersonId = p.PersonId});
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int personId, int departmentId)
        {
            var entity = await _context.PersonsDepartments
                .Where(entity => entity.PersonId == personId && entity.DepartmentId == departmentId)
                .FirstOrDefaultAsync();
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            } 
        }

        public async Task UpdateAsync(IReadOnlyList<PersonDepartmentRequest> item)
        {
            foreach (var p in item.ToList())
            {

                var entity = await _context
                               .PersonsDepartments
                               .Where(entity => entity.Id == p.Id)
                               .FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.PersonId = p.PersonId;
                    entity.DepartmentId = p.DepartmentId;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
