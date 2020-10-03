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
        [Display(Name = "Date and time of the event")]
        public DateTime Time { get; set; }

        // only adress should be given and after that new place with the given
        // adress should be generated in service
        public virtual Place Place { get; set; }

        [Display(Name = "Max Visitors count (not required)")]
        [Range(1, int.MaxValue)]
        public int? VisitorsCount { get; set; }

        [Required]
        [Display(Name = "Entrance fee in Lv")]
        [Range (1, int.MaxValue)]
        public int EntranceFee { get; set; }

        public EntranceType EntranceType { get; set; }
    }
}
