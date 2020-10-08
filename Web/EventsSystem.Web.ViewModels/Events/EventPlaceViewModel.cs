using AutoMapper;
using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;
using EventsSystem.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.Events
{
    public class EventPlaceViewModel : IMapFrom<Place>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Adress { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Place, IndexPlaceViewModel>()
                         .ForMember(
                             x => x.Url,
                             c => c.MapFrom(e => "/" + e.Name.Replace(' ', '-')));
        }
    }
}
