using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsSystem.Web.ViewModels.CreateEvent
{
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
        public string City { get; set; }


    }
}
