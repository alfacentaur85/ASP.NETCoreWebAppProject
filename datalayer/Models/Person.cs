using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace datalayer.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
        public List<PersonDepartment> PersonDepartment { get; set; } = new List<PersonDepartment>();


    }
}
