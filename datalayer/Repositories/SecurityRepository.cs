using System;
using System.Collections.Generic;
using datalayer.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using datalayer.Responses;
using datalayer.Requests;
using Secutrity;

namespace datalayer.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private ApplicationDataContext _context;

        public SecurityRepository(ApplicationDataContext context)
        {
            _context = context;
        }


        public async Task<IReadOnlyList<UserResponse>> GetByLoginPasswordAsync(string login, string password)
        {
            return await _context.Users
                .Select(p => new UserResponse() { Id = p.Id, UserName = p.UserName, Password = p.Password, refreshToken = new RefreshToken() { id = p.refreshToken.id, Expires = p.refreshToken.Expires, Token = p.refreshToken.Token } })
                .Where(p => p.UserName.Equals(string.IsNullOrEmpty(login) ? "" : login)  && p.Password.Equals(string.IsNullOrEmpty(password) ? "" : password))
                .ToArrayAsync();
        }

    }
}
