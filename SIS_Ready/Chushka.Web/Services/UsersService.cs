using Chushka.Data;
using Chushka.Models;
using Chushka.Models.Enums;
using Chushka.Web.Services.Contracts;
using System;
using System.Linq;

namespace Chushka.Web.Services
{
    public class UsersService : IUsersService
    {
        private readonly ChushkaContext context;

        public UsersService(ChushkaContext context)
        {
            this.context = context;
        }

        public User GetUserByUsername(string username)
        => this.context.Users.First(x => x.Username == username);

        public void RegisterUser(string username, string password, string fullName, string email)
        {
            var role = this.IsFirstUser() ? Role.User : Role.Admin;

            var newUser = new User
            {
                Username = username,
                Password = password,
                Email = email,
                FullName = fullName,
                Role = role
            };

            this.context.Users.Add(newUser);
            this.context.SaveChanges();
        }

        public bool UserExistsByUsernameAndPassword(string username, string password)
        => this.context.Users.Any(x => x.Username == username && x.Password == password);

        private bool IsFirstUser() => this.context.Users.Any();

    }
}
