using System;
using System.Collections.Generic;
using System.Text;

namespace datalayer.Requests
{
    public class PersonDepartmentRequest
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int DepartmentId { get; set; }
    }
}
