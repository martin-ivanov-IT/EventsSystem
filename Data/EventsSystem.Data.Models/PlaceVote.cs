﻿namespace EventsSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using EventsSystem.Data.Common.Models;
    using EventsSystem.Data.Models;

    public class PlaceVote : BaseModel<int>
    {
        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType Type { get; set; }
    }
}
