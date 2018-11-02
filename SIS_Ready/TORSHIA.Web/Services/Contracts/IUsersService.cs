using System;
using System.Collections.Generic;
using System.Text;
using TORSHIA.Models;

namespace TORSHIA.Web.Services.Contracts
{
    public interface IUsersService
    {
        void RegisterUser(string username, string password, string confirmPassword, string email);

        bool UserExistsByUsernameAndPassword(string username, string password);

        User GetUserByUsername(string username);    
    }
}
