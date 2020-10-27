namespace EventsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventsSystem.Data.Models;
    using EventsSystem.Services.Data;
    using EventsSystem.Web.ViewModels.Votes;
    using EventsSystem.Web.Views.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IPlaceVotesService placeVotesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventVotesService eventVotesService;

        public VotesController(
            IPlaceVotesService placeVotesService,
            UserManager<ApplicationUser> userManager,
            IEventVotesService eventVotesService)
        {
            this.placeVotesService = placeVotesService;
            this.userManager = userManager;
            this.eventVotesService = eventVotesService;
        }

        // POST/api/votes
        // Request: {"postId":1,"isUpVote":true}
        // Response: {"votesCount" :16}
        [Authorize]
        [HttpPost]
        [Route("api/[controller]place")]
        public async Task<ActionResult<VoteResponseModel>> Post(PlaceVoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.placeVotesService.VoteAsync(input.PlaceId, userId, input.IsUpVote);
            var votes = this.placeVotesService.GetVotes(input.PlaceId);
            return new VoteResponseModel { VotesCount = votes };
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]event")]
        public async Task<ActionResult<VoteResponseModel>> PostEvent(EventVoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.eventVotesService.VoteAsync(input.EventId, userId, input.IsUpVote);
            var votes = this.eventVotesService.GetVotes(input.EventId);
            return new VoteResponseModel { VotesCount = votes };
        }
    }
}
