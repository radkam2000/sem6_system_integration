using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_LAB8.Entities;
using IS_LAB8.Model;

namespace IS_LAB8.Services.Users
{
    public interface IUserService
    {
        AuthenticationResponse
        Authenticate(AuthenticationRequest request);
        IEnumerable<User> GetUsers();
        User GetByUsername(string username);
        User GetById(int id);
        int GetRegisteredUsersCount();
    }
}
