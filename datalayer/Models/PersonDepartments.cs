using System.ComponentModel.DataAnnotations.Schema;

namespace datalayer.Models
{
    public class PersonDepartment
    {
        public int Id { get; set; }

        public int PersonInfoKey { get; set; }
        [ForeignKey("PersonInfoKey")]
        public Person Person { get; set; }

        public int DepartmentInfoKey { get; set; }
        [ForeignKey("DepartmentInfoKey")]
        public Department Department { get; set; }

    }
}
