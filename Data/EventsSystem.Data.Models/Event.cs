using EventsSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class Event : BaseDeletableModel<int>
    {
        public DateTime Time { get; set; }

        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public int VisitorsCount { get; set; }

        public double EntranceFee { get; set; }

        public int CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }


    }
}
