namespace EventsSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EventsSystem.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public string Path { get; set; }

        public int PlaceId { get; set; }

        public Place Place { get; set; }
    }
}
