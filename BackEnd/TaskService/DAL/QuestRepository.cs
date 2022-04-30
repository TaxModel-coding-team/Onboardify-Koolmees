﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using back_end.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace back_end.DAL
{
    public class QuestRepository : IQuestRepository
    {
        private readonly QuestContext _context;

        public QuestRepository(QuestContext context)
        {
            _context = context;
        }

        public ICollection<Quest> GetAllQuests()
        {
            List<Quest> quests = new List<Quest>();
            quests = _context.Quest.Include(q => q.SubQuests).ToList();

            return quests;
        }

        //public ICollection<Quest> GetQuestsByRole(Guid guid)
        //{
        //    List<Quest> quests = new List<Quest>();
        //    quests.Add(_context.Quest.FirstOrDefault(q => q.RoleId == guid));
        //    return quests;
        //}

        public void NewUserQuests(List<QuestUserManagement> questUserManagement)
        {
            _context.QuestUserManagement.AddRange(questUserManagement);
            _context.SaveChanges();
        }

        public ICollection<QuestUserManagement> GetSubQuestsByUser(Guid guid)
        {
            List<QuestUserManagement> questUserManagement = new List<QuestUserManagement>();

            questUserManagement = _context.QuestUserManagement.Where(u => u.UserID == guid).Include(q => q.SubQuest)
                .ToList();

            return questUserManagement;
        }

        //public ICollection<Quest> GetQuestBySubQuest(List<SubQuest> subQuests)
        //{
        //    List<Quest> quests = new List<Quest>();

        //    foreach (SubQuest subQuest in subQuests)
        //    {
        //        quests.Add(_context.Quest.Where(q => q.SubQuests.Any(sq => sq.ID == subQuest.ID)).FirstOrDefault());
        //    }

        //    return quests.Distinct().ToList();
        //}

        public bool CompleteQuest(QuestUserManagement questToComplete)
        {
            QuestUserManagement result = _context.QuestUserManagement.SingleOrDefault(questUser =>
                questUser.UserID == questToComplete.UserID && questUser.SubQuestID == questToComplete.SubQuestID);

            if (result != null)
            {
                result.Completed = true;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public List<Quest> GetFullQuestsByRoles(List<Role> roles)
        {
            List<Guid> ids = new List<Guid>();
            foreach (var role in roles)
            {
                ids.Add(role.RoleID);
            }
            List<Quest> quests = new List<Quest>();
            quests = _context.Quest.Where(q => ids.Contains(q.RoleId)).ToList();
            return quests;
        }
    }
}