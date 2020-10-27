namespace EventsSystem.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Mapping;

   public class IndexEventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public virtual Place Place { get; set; }

        public IEnumerable<Image> PlaceImages { get; set; }

        public string Url { get; set; }

        public int VisitorsCount { get; set; }

        public int EntranceFee { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, IndexEventViewModel>()
                         .ForMember(
                             x => x.Url,
                             c => c.MapFrom(e => "/" + e.Name.Replace(' ', '-')));

            configuration.CreateMap<Place, IndexEventViewModel>()
                         .ForMember(
                             x => x.PlaceImages,
                             c => c.MapFrom(e => new Place
                             {
                             Images = e.Images,
                             }));
        }
    }
}
