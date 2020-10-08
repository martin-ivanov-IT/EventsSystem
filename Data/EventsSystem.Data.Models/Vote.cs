﻿using EventsSystem.Data.Common.Models;
using EventsSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class Vote : BaseModel<int>
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
