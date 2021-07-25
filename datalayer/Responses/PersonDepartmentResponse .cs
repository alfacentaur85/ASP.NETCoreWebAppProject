using System;
using System.Collections.Generic;
using System.Text;
using datalayer.Models;

namespace datalayer.Responses
{
    public class PersonDepartmentResponse
    {
        public int PersonId { get; set; }
        public int DepartmentId { get; set; }
        public List<Person> Persons { get; set; } = new List<Person>();
        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
