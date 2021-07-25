using System;
using System.Collections.Generic;
using System.Text;
using datalayer.Models;

namespace datalayer.Responses
{
    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Person> Persons { get; set; } = new List<Person>();
    }
}
