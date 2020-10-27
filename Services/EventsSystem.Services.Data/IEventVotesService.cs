namespace EventsSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using AngleSharp.Dom.Events;

    public interface IEventVotesService
    {
        Task VoteAsync(int eventId, string userId, bool isUpVote);

        int GetVotes(int eventId);
    }
}
