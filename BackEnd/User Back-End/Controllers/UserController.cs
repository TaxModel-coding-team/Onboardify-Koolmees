﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using User_Back_End.Logic;
using User_Back_End.Models;
using User_Back_End.ViewModels;

namespace User_Back_End.Controllers
{
    [Route("users")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserLogic _userLogic;
        static HttpClient client = new HttpClient();

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:44329/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public UserController(UserLogic userlogic)
        {
            _userLogic = userlogic;
            RunAsync();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<User>> Login([FromBody] UserViewModel userViewModel)
        {
            userViewModel = _userLogic.GetUser(userViewModel);
            if (userViewModel != null)
            {
                return Ok(userViewModel);
            }
            return StatusCode(404, "User doesn't exist");
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<UserViewModel>> CreateUser([FromBody] UserViewModel userViewModel)
        {
            if (userViewModel.Username != null && userViewModel.Email != null)
            {
                userViewModel = _userLogic.NewUser(userViewModel);
                await CreateUserQuests(userViewModel);
                return CreatedAtAction("CreateUser", userViewModel);
            }
            return StatusCode(404, "Not all fields are filled in");
        }

        static async Task<Uri> CreateUserQuests(UserViewModel userViewModel)
        {
            NewUserViewModel newUserViewModel = new NewUserViewModel();
            newUserViewModel.ID = userViewModel.ID;
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:5001/quests/assignQuests", newUserViewModel);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }

        [HttpGet]
        [Route("byRole/{Id}")]
        public IActionResult GetUsersByRole(Guid id)
        {
            var users = _userLogic.GetUsersByRole(id);
            return Ok(users);
        }
        [HttpGet]
        [Route("getRoles/{userID}")]
        public IActionResult GetRolesByUser(Guid userID)
        {
            UserViewModel userVM = _userLogic.GetRolesByUserID(userID);
           // ICollection<UserRoleViewModel> roles = userVM.Roles;
            return Ok(userVM);
        }

    }
}
