using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Back_End.Models;

namespace User_Back_End.DAL
{
    public class RoleRepository
    {
        private readonly UserContext _context;
        public RoleRepository(UserContext context)
        {
            _context = context;
        }

        public Role GetRole(Role role)
        {
            return _context.Role.SingleOrDefault(q => q.Id == role.Id);
        }

        public Role NewRole(Role role)
        {
            _context.Role.Add(role);
            _context.SaveChanges();
            return role;
        }
    }
}
