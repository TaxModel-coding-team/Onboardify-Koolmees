using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using User_Back_End.Models;
using User_Back_End.ViewModels;

namespace User_Back_End.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleViewModel>().ReverseMap();
        }
    }
}
