using AutoMapper;
using back_end.Models;
using back_end.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Role, UserRoleViewModel>().ReverseMap();
        }
    }
}
