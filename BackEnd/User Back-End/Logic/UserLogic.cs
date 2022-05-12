using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Back_End.DAL;
using User_Back_End.Models;
using User_Back_End.ViewModels;
using System.Drawing;

namespace User_Back_End.Logic
{
    public class UserLogic
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserLogic(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UserViewModel GetUser(UserViewModel userViewModel)
        {
            User user = _mapper.Map<User>(userViewModel);
            userViewModel = _mapper.Map<UserViewModel>(_repository.GetUser(user));           
            return userViewModel;
        }

        public UserViewModel NewUser(UserViewModel userViewModel)
        {   
           
            var user = _mapper.Map<User>(userViewModel);
            userViewModel =  _mapper.Map<UserViewModel>(_repository.NewUser(user));
           
            return userViewModel;
        }

        public List<UserViewModel> GetUsersByRole(Guid id)
        {
            List<User> users = _repository.GetByRole(id).ToList();
            List<UserViewModel> userViewModels = _mapper.Map<List<UserViewModel>>(users);
            return userViewModels;
        }

        public UserViewModel GetRolesByUserID(Guid id)
        {
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(_repository.GetRolesByUser(id));
            return userViewModel;
        }
    }
}
