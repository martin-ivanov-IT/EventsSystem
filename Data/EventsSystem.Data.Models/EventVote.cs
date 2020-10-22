using EventsSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class EventVote : BaseModel<int>
    {
        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType Type { get; set; }
    }
}
