using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User_Back_End.Models
{
    public class User
    {
        [Key] [Required] public Guid ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<Role> Roles { get; set; }

        public User()
        {
        }

        public User(Guid id, string email, string username, List<Role> roles)
        {
            ID = id;
            Email = email;
            Username = username;
            Roles = roles;
        }

        public User(string email)
        {
            Email = email;
        }

        public User(Guid id, string email)
        {
        }
    }
}