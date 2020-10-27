namespace EventsSystem.Web.ViewModels.Places
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EventsSystem.Web.ViewModels.Events;

    public class PlaceWithEventViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }

        public PlaceViewModel Place { get; set; }
    }
}
