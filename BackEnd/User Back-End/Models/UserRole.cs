using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User_Back_End.Models
{
    public class UserRole
    {
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public virtual User user { get; set; }
        public virtual Role role { get; set; }
    }
}
