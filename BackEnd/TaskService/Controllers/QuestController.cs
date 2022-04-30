using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using back_end.ViewModels;
using back_end.DAL;
using back_end.Logic;
using AutoMapper;
using System.Net.Http;
using System.Net.Http.Headers;

namespace back_end.Controllers
{
    [Route("quests")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        

        private readonly QuestLogic _questlogic;
        static HttpClient client = new HttpClient();

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:44329/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public QuestController(QuestLogic questlogic)
        {
            _questlogic = questlogic;
            RunAsync();
        }


        [HttpGet]
        [Route("{userID}")]
        public async Task<ActionResult> GetQuestsByUserAsync(Guid userID)
        {
            UserViewModel userViewModel =  await GetUserRoles(userID);
            List<QuestViewModel> quests = _questlogic.GetUserQuests(userViewModel);

            return Ok(quests);
        }

        [HttpPut]
        [Route("complete")]
        public IActionResult CompleteQuest([FromBody] QuestCompletionViewModel completedQuest)
        {
            return Ok(_questlogic.CompleteQuest(completedQuest));
        }

        [HttpPost]
        [Route("assignQuests")]
        public ActionResult<UserViewModel> AssignQuests([FromBody] UserViewModel userViewModel)
        {
            List<QuestViewModel> quests =  _questlogic.GetAllQuests();
            List<QuestCompletionViewModel> questCompletionViewModels = new List<QuestCompletionViewModel>();
            foreach(QuestViewModel quest in quests)
            {
                AssignQuestForUser(userViewModel, questCompletionViewModels, quest);
            }
            _questlogic.NewUserQuests(questCompletionViewModels);
            return Ok(quests);
        }

        //[HttpGet]
        //[Route("role/{ID}")]
        //public IActionResult GetQuestsByRole(Guid ID)
        //{
        //    var quests = _questlogic.GetQuestsByRole(ID);
        //    return Ok(quests);
        //}

        private void AssignQuestForUser(UserViewModel userViewModel, List<QuestCompletionViewModel> questCompletionViewModels, QuestViewModel quest)
        {
            foreach (SubQuestViewModel subQuestViewModel in quest.SubQuests)
            {
                QuestCompletionViewModel questCompletionViewModel = new QuestCompletionViewModel();
                questCompletionViewModel.UserID = userViewModel.ID;
                questCompletionViewModel.SubQuestID = subQuestViewModel.ID;
                questCompletionViewModels.Add(questCompletionViewModel);
            }
        }
        

        static async Task<UserViewModel> GetUserRoles(Guid ID)
        {
            List<UserRoleViewModel> userRoleViewModels = new List<UserRoleViewModel>();
            string requestURI = "https://localhost:5005/users/getRoles/" + ID.ToString();
            UserViewModel userViewModel = new UserViewModel();
            HttpResponseMessage response = client.GetAsync(requestURI).Result;
            if (response.IsSuccessStatusCode)
            {
                userViewModel = response.Content.ReadAsAsync<UserViewModel>().Result;
            }
               // Add Error handling
            return userViewModel;
        }
    }
}
