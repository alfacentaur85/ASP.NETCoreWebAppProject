using System;
using System.Collections.Generic;
using System.Text;
using datalayer.Models;
using Core;
using datalayer.Responses;
using datalayer.Requests;

namespace datalayer.Interfaces
{
    public interface IUserRepository : IRepository<UserResponse, UserRequest>
    {

    }
}
