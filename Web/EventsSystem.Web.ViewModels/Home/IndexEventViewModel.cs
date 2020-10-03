﻿using AutoMapper;
using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.Home
{
   public class IndexEventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }

        public virtual Place Place { get; set; }

        public string Url { get; set; }

        public int VisitorsCount { get; set; }

        public int EntranceFee { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, IndexEventViewModel>()
                         .ForMember(
                             x => x.Url,
                             c => c.MapFrom(e => "/" + e.Name.Replace(' ', '-')));
        }
    }
}
