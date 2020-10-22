using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.Votes
{
    public class EventVoteInputModel
    {
        public int EventId { get; set; }

        public bool IsUpVote { get; set; }
    }
}
