using EventsSystem.Data.Models;
using EventsSystem.Web.Views.Votes;
using EventsSystem.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsSystem.Web.ViewModels.Votes;

namespace EventsSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        // POST/api/votes
        // Request: {"postId":1,"isUpVote":true}
        // Response: {"votesCount" :16}
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.votesService.VoteAsync(input.PlaceId, userId, input.IsUpVote);
            var votes = this.votesService.GetVotes(input.PlaceId);
            return new VoteResponseModel { VotesCount = votes };
        }
    }
}
