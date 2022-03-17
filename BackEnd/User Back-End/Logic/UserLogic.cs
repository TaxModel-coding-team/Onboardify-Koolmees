﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Back_End.DAL;
using User_Back_End.Models;
using User_Back_End.ViewModels;
using IronBarCode;
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
            if (userViewModel != null)
            {
                userViewModel.qrCode = CreateQRCode(userViewModel);
            }
            return userViewModel;
        }

        public UserViewModel NewUser(UserViewModel userViewModel)
        {   
            userViewModel.ExperiencePoints = 0;
            var user = _mapper.Map<User>(userViewModel);
            userViewModel =  _mapper.Map<UserViewModel>(_repository.NewUser(user));
            userViewModel.qrCode = CreateQRCode(userViewModel);
            return userViewModel;
        }

        public Image CreateQRCode(UserViewModel userViewModel)
        {       
            return QRCodeWriter.CreateQrCode(userViewModel.ID.ToString(), 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).ToImage();
        }

        public List<UserViewModel> GetUsersByRole(Guid id)
        {
            List<User> users = _repository.GetByRole(id).ToList();
            List<UserViewModel> userViewModels = _mapper.Map<List<UserViewModel>>(users);
            return userViewModels;
        }
    }
}
