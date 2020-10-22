using AngleSharp.Dom.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsSystem.Services.Data
{
    public interface IEventVotesService
    {
        Task VoteAsync(int eventId, string userId, bool isUpVote);

        int GetVotes(int eventId);
    }
}
