using AutoMapper;
using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;
using EventsSystem.Web.ViewModels.CreateEvent;
using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.Events
{
   public class EventViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public virtual Place Place { get; set; }

        public int PlaceId { get; set; }

        public string PlaceUrl { get; set; }

        public string Url { get; set; }

        public int VisitorsCount { get; set; }

        public int EntranceFee { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                         .ForMember(
                             x => x.Url,
                             c => c.MapFrom(e => "/" + e.Name.Replace(' ', '-')))
                          .ForMember(
                             x => x.PlaceUrl,
                             c => c.MapFrom(e => "p/" + e.Place.Name.Replace(' ', '-')));


        }

    }
}

