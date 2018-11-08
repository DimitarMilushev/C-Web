using Chushka.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Web.Services.Contracts
{
    public interface IUsersService
    {
        void RegisterUser
            (string username, string password, string fullName, string email);

        bool UserExistsByUsernameAndPassword(string username, string password);

        User GetUserByUsername(string username);
    }
}
