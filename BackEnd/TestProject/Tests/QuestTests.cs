using AutoMapper;
using back_end.DAL;
using back_end.Logic;
using back_end.Models;
using back_end.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Stub;

namespace TestProject.Tests
{
    [TestClass]
    public class QuestTests
    {
        private readonly QuestLogic _logic;
        private readonly IMapper _mapper;

        public QuestTests()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Quest, QuestViewModel>().ReverseMap();
                cfg.CreateMap<SubQuest, SubQuestViewModel>().ReverseMap();
                cfg.CreateMap<QuestUserManagement, QuestCompletionViewModel>().ReverseMap();
                cfg.CreateMap<List<SubQuest>, List<SubQuestViewModel>>().ReverseMap();
            });

            _mapper = new Mapper(config);
            IQuestRepository IQuestRepository = new QuestStub();
            _logic = new QuestLogic(IQuestRepository, _mapper);
        }
    }
}
