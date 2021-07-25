using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using datalayer.Models;
using datalayer.DTO;

namespace datalayer
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<PersonDepartmentDTO>();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<PersonDepartment> PersonDepartments { get; set; }

        public DbSet<PersonDepartmentDTO> PersonDepartmentDTO { get; set; }
    }
}