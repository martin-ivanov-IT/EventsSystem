using EventsSystem.Web.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.Places
{
    public class PlaceWithEventViewModel
    {
        public IEnumerable<EventViewModel> Events { get; set; }

        public PlaceViewModel Place { get; set; }
    }
}
