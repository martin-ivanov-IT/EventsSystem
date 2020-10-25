namespace EventsSystem.Web.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Mapping;
    using EventsSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Http;

    public partial class CreateEventInputModel : IMapFrom<Event>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date and time of the event")]

        public DateTime Time { get; set; }

        [Display(Name = "Entrance fee in Lv")]
        [Range (1, int.MaxValue)]
        public int? EntranceFee { get; set; }

        public EntranceType EntranceType { get; set; }

        [Required]
        public string PlaceCity { get; set; }

        [Required]
        public string PlaceName { get; set; }

        public IEnumerable <IFormFile> Photos { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, CreateEventInputModel>()
                         .ForMember(
                             x => x.PlaceCity,
                             c => c.MapFrom(e => e.Place.Address))
                         .ForMember(
                             x => x.PlaceName,
                             c => c.MapFrom(e => e.Place.Name));
        }

    }
}
