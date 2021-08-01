using Microsoft.EntityFrameworkCore;
using datalayer.Models;
using Secutrity;

namespace datalayer
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> Persons { get; set; }

        public DbSet<PersonDepartment> PersonsDepartments { get; set; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Person>()
                .HasMany(c => c.Departments)
                .WithMany(s => s.Persons)
                .UsingEntity<PersonDepartment>(
                   j => j
                    .HasOne(pt => pt.Department)
                    .WithMany(t => t.PersonDepartment)
                    .HasForeignKey(pt => pt.DepartmentId),
                   j => j
                    .HasOne(pt => pt.Person)
                    .WithMany(p => p.PersonDepartment)
                    .HasForeignKey(pt => pt.PersonId),
                   j =>
                {
                    j.HasKey(t => new { t.PersonId, t.DepartmentId });
                    j.ToTable("PersonDepartment");
                });
            modelBuilder.Entity<User>()
              .HasIndex(u => u.UserName)
              .IsUnique();
        }

    }
}