namespace EventsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventsSystem.Data;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Data;
    using EventsSystem.Web.ViewModels;
    using EventsSystem.Web.ViewModels.AllFreinds;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PeopleController: Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public PeopleController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public async Task<IActionResult> AllFriends()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var friendships = this.db.Friendships.Where(u => u.FriendFrom.Email.Equals(user.Email))
                                                 .Select(e => new Friendship
                                                {
                                                  FriendFrom = e.FriendFrom,
                                                  FriendTo = e.FriendTo,
                                                }).ToList();

            var viewModel = new FriendShipViewModel();
            viewModel.FriendshipsModel = friendships;
            return this.View(viewModel);
        }
    }
}
