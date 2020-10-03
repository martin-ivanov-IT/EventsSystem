using AutoMapper;
using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;

namespace EventsSystem.Web.ViewModels.Home
{
    public class IndexPlaceViewModel : IMapFrom<Place>, IHaveCustomMappings
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
