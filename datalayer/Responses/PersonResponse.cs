using System;
using System.Collections.Generic;
using System.Text;
using datalayer.Models;

namespace datalayer.Responses
{
    public class PersonResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
