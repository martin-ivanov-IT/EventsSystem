﻿namespace EventsSystem.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Mapping;
    using Ganss.XSS;

   public class EventViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public virtual Place Place { get; set; }

        public IEnumerable<Image> PlaceImages { get; set; }

        public int PlaceId { get; set; }

        public string PlaceUrl { get; set; }

        public int VisitorsCount { get; set; }

        public int EntranceFee { get; set; }

        public int VotesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                         .ForMember(x => x.VotesCount, options =>
                         {
                             options.MapFrom(p => p.Votes.Sum(v => (int)v.Type));
                         });
            configuration.CreateMap<Place, EventViewModel>()
                         .ForMember(
                             x => x.PlaceImages,
                             c => c.MapFrom(e => new Place
                             {
                                 Images = e.Images,
                             }));
        }
    }
}
