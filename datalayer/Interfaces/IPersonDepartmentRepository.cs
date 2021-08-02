using System;
using System.Collections.Generic;
using System.Text;
using Core;
using datalayer.Requests;
using datalayer.Responses;

namespace datalayer.Interfaces
{
    public interface IPersonDepartmentRepository : IRepositoryPD<PersonDepartmentResponse, PersonDepartmentRequest>
    {
    }
}
