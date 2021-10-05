using System;
using System.Collections.Generic;
using System.Text;

namespace datalayer.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Person> Persons { get; set; } = new List<Person>();
        public List<PersonDepartment> PersonDepartment { get; set; } = new List<PersonDepartment>();
    }
}
