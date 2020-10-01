using EventsSystem.Data.Common.Models;
using EventsSystem.Web.ViewModels.CreateEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class Event : BaseDeletableModel<int>
    {

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public int VisitorsCount { get; set; }

        public int EntranceFee { get; set; }

        public EntranceType EntranceType { get; set; }
    }
}
