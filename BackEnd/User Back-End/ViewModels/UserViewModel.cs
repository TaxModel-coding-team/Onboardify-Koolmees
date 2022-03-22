using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using User_Back_End.Models;

namespace User_Back_End.ViewModels
{
    public class UserViewModel
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int ExperiencePoints { get; set; }
        public Image qrCode { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
