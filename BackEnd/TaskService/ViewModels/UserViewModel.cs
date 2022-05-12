using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.ViewModels
{
    public class UserViewModel
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<UserRoleViewModel> roles { get; set; }
    }
}
