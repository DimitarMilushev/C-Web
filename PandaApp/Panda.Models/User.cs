using Panda.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Panda.Models
{
    public class User
    {
        public User()
        {
            this.Packages = new HashSet<Package>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Package> Packages { get; set; }
    }
}
