using EventsSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class EventReview : BaseModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public int PlaceId { get; set; }

        public Place Place { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


    }
}
