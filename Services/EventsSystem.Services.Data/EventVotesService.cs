namespace EventsSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AngleSharp.Dom.Events;
    using EventsSystem.Data.Common.Repositories;
    using EventsSystem.Data.Models;

    public class EventVotesService : IEventVotesService
    {
        private readonly IRepository<EventVote> votesRepository;

        public EventVotesService(IRepository<EventVote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotes(int eventId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.EventId == eventId).Sum(x => (int)x.Type);
            return votes;
        }

        public async Task VoteAsync(int eventId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
               .FirstOrDefault(x => x.EventId == eventId && x.UserId == userId);

            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.Neutral;
            }
            else
            {
                vote = new EventVote
                {
                    EventId = eventId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.Neutral,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
