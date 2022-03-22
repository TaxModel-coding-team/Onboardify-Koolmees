using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using User_Back_End.Models;
using User_Back_End.ViewModels;

namespace User_Back_End.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            //source <--> target
            CreateMap<Role, RoleViewModel>().ReverseMap();
        }

    }
}
