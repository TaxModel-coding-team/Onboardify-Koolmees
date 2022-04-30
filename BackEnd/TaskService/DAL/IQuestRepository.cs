using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Models;
using back_end.ViewModels;


namespace back_end.DAL
{
    public interface IQuestRepository
    {
        ICollection<QuestUserManagement> GetSubQuestsByUser(Guid guid);
       // ICollection<Quest> GetQuestBySubQuest(List<SubQuest> subQuests);
        ICollection<Quest> GetAllQuests();
       // ICollection<Quest> GetQuestsByRole(Guid guid);
        bool CompleteQuest(QuestUserManagement questToComplete);

        void NewUserQuests(List<QuestUserManagement> questUserManagement);
        List<Quest> GetFullQuestsByRoles(List<Role> roles);
    }
}
