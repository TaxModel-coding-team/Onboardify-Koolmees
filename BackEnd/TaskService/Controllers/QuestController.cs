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
        [Route("{ID}")]
        public IActionResult GetQuestsByUser(Guid ID)
        {
            var quests = _questlogic.GetQuestsByUser(ID);
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

        [HttpGet]
        [Route("role/{ID}")]
        public IActionResult GetQuestsByRole(Guid ID)
        {
            var quests = _questlogic.GetQuestsByRole(ID);
            return Ok(quests);
        }

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

        static async Task<Uri> GetUserRoles(Guid userID)
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.ID = userID;
            HttpResponseMessage response = await client.GetAsync(
                "https://localhost:5005/GetRoles/{userID}");
           // userViewModel = response;
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }
    }
}
