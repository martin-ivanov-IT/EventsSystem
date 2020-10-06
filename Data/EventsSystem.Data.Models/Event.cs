using EventsSystem.Data.Common.Models;
using EventsSystem.Web.ViewModels.CreateEvent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.Reviews = new HashSet<EventReview>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Time { get; set; }

        public int? EntranceFee { get; set; }

        public EntranceType EntranceType { get; set; }

        public int PlaceId { get; set; }

        public Place Place { get; set; }

        public string PlaceUrl { get; set; }

        [Required]
        public int InitiatorId { get; set; }

        public virtual ApplicationUser Initiator { get; set; }

        public virtual ICollection<EventReview> Reviews { get; set; }

        // many to many - users interested
    }
}
