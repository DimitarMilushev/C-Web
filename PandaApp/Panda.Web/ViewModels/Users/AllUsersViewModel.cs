using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Web.ViewModels.Users
{
    public class AllUsersViewModel
    {
        public IEnumerable<UsernameViewModel> Recipients { get; set; }
    }
}
