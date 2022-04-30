using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back_end.DAL;
using back_end.Models;
using back_end.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace back_end.Logic
{
    public class QuestLogic
    {
        private readonly IQuestRepository _repository;
        private readonly IMapper _mapper;

        public QuestLogic(IQuestRepository repository, IMapper mapper)
        {
            this._repository = repository;
            _mapper = mapper;
        }

        public void NewUserQuests(List<QuestCompletionViewModel> newUserQuests)
        {
            List<QuestUserManagement> userManagements = _mapper.Map<List<QuestUserManagement>>(newUserQuests);
            _repository.NewUserQuests(userManagements);
        }

        public List<QuestViewModel> GetAllQuests()
        {
            List<Quest> quests = _repository.GetAllQuests().ToList();
            List<QuestViewModel> questViewModels = _mapper.Map<List<QuestViewModel>>(quests);

            return questViewModels;
        }

        public List<QuestViewModel> GetQuestsByUser(Guid guid)
        {
            List<QuestUserManagement> questsUsers = _repository.GetSubQuestsByUser(guid).ToList();


            foreach (QuestUserManagement quest in questsUsers)
            {
                quest.SubQuest.Completed = quest.Completed;
            }

            List<Quest> quests = _repository.GetQuestBySubQuest(questsUsers.Select(q => q.SubQuest).ToList()).ToList();
            List<QuestViewModel> questViewModels = _mapper.Map<List<QuestViewModel>>(quests);

            return questViewModels;
        }

        public List<QuestViewModel> GetUserQuests(UserViewModel userViewModel)
        {
            List<QuestUserManagement> questUserManagements = _repository.GetSubQuestsByUser(userViewModel.ID).ToList();
            List<QuestViewModel> quests = _mapper.Map<List<QuestViewModel>>(_repository.GetFullQuestsByRoles(_mapper.Map<List<Role>>(userViewModel.roles)));

            foreach (QuestViewModel quest in quests)
            {
                
                foreach (SubQuestViewModel subQuestViewModel in quest.SubQuests)
                {
                    if (questUserManagements.Any(q => q.SubQuestID == subQuestViewModel.ID && q.Completed == true))
                    {
                        subQuestViewModel.Completed = true;
                    }
                }              
            }
            return quests;
        }

        public List<QuestViewModel> GetQuestsByRole(Guid guid)
        {
            List<Quest> quests = _repository.GetQuestsByRole(guid).ToList();
            List<QuestViewModel> questViewModels = _mapper.Map<List<QuestViewModel>>(quests);
            return questViewModels;
        }

        public bool CompleteQuest(QuestCompletionViewModel questToComplete)
        {
            return _repository.CompleteQuest(_mapper.Map<QuestUserManagement>(questToComplete));
        }
    }
}