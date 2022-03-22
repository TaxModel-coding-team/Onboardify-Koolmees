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

namespace back_end.Controllers
{
    [Route("quests")]
    [ApiController]
    public class QuestController : ControllerBase
    {

        private readonly QuestLogic _questlogic;

        public QuestController(QuestLogic questlogic)
        {
            _questlogic = questlogic;
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

        // [HttpGet]
        // [Route("byRoles")]
        // public IActionResult GetQuestsByRoles(string ids)
        // {
        //     var separated = ids.Split(new char[] { ',' });
        //     List<Guid> parsed = separated.Select(Guid.Parse).ToList();
        //     List<RoleViewModel> roleViewModels = new List<RoleViewModel>();
        //     foreach (Guid guid in parsed)
        //     {
        //         RoleViewModel model = new RoleViewModel();
        //         model.Id = guid;
        //         roleViewModels.Add(model);
        //     }
        //
        //     var response = _questlogic.GetQuestsByRole(roleViewModels);
        //     return Ok(response);
        // }

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
    }
}
