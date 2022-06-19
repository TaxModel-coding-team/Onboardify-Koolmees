using back_end.DAL;
using back_end.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Stub
{
    class QuestStub : IQuestRepository
    {
        private List<Quest> quests = new List<Quest>();
        public QuestStub()
        {
            CreateQuests();
        }
    }
}
