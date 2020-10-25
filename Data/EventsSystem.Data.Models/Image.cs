using EventsSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class Image : BaseDeletableModel<int>
    {
        public string Path { get; set; }

        public int PlaceId { get; set; }

        public Place Place { get; set; }
    }
}
