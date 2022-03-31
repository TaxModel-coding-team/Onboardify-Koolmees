using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Back_End.Models;

namespace User_Back_End.DAL
{
    public interface IRoleRepository
    {
        Role GetRole(Role role);

        Role NewRole(Role role);
        List<Role> GetAllRoles();
    }
}
