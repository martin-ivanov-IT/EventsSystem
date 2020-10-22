using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.Places
{
   public class PlaceAllViewModel
    {
        public IEnumerable<PlaceViewModel> AllPlaces { get; set; }

        public IEnumerable<PlaceViewModel> PlacesByCity { get; set; }
    }
}
