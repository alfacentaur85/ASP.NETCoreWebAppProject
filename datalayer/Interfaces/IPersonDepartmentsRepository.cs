using System;
using System.Collections.Generic;
using System.Text;
using Core;
using datalayer.DTO;
using datalayer.Models;

namespace datalayer.Interfaces
{
    public interface IPersonDepartmentsRepository : IRepositoryPD<PersonDepartment, PersonDepartmentDTO>
    {
    }
}
