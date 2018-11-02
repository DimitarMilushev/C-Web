using System.Linq;
using TORSHIA.Data;
using TORSHIA.Models;
using TORSHIA.Models.Enums;
using TORSHIA.Web.Services.Contracts;

namespace TORSHIA.Web.Services
{
    public class UsersServices : IUsersService
    {
        private readonly TorshiaContext context;

        public UsersServices(TorshiaContext context)
        {
            this.context = context;
        }
        
        public void RegisterUser(string username, string password, string confirmPassword, string email)
        {
            var role = IsFirstGuest() ? Role.User : Role.Admin;

            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Role = role
            };
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public bool UserExistsByUsernameAndPassword(string username, string password)
        {
            return this.context.Users.Any(x => x.Username == username && x.Password == password);
        }

        public User GetUserByUsername(string username)
        => this.context.Users.FirstOrDefault(x => x.Username == username);

        //private bool IsPasswordValid(string password, string confirmPassword)
        //{
        //    if (password == confirmPassword)
        //        return true;

        //    return false;
        //}

        private bool IsFirstGuest()
        {
            return this.context.Users.Any();
        }
    }
}
