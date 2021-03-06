﻿namespace EventsSystem.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using EventsSystem.Data.Common.Repositories;
    using EventsSystem.Data.Models;
    using EventsSystem.Data.Models;

    public class PlaceVotesService : IPlaceVotesService
    {
        private readonly IRepository<PlaceVote> votesRepository;

        public PlaceVotesService(IRepository<PlaceVote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotes(int placeId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.PlaceId == placeId).Sum(x => (int)x.Type);
            return votes;
        }

        public async Task VoteAsync(int placeId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.PlaceId == placeId && x.UserId == userId);
            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new PlaceVote
                {
                    PlaceId = placeId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
