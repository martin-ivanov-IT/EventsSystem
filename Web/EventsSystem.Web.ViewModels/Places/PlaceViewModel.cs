using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.Places
{
    public class PlaceViewModel : IMapFrom<Place>
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<PlaceReview> Reviews { get; set; }

    }
}
