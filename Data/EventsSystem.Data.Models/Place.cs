﻿using EventsSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class Place : BaseDeletableModel<int>
    {
        public Place()
        {
            this.Events = new HashSet<Event>();
            this.Reviews = new HashSet<PlaceReview>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<PlaceReview> Reviews { get; set; }
    }
}
