using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Views.Votes
{
    public class PlaceVoteInputModel
    {
        public int PlaceId { get; set; }

        public bool IsUpVote { get; set; }
    }
}
