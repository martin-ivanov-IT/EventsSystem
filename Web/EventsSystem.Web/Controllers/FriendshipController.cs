namespace EventsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventsSystem.Data;
    using EventsSystem.Data.Models;
    using EventsSystem.Web.ViewModels.FriendShip;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class FriendshipController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public FriendshipController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public async Task<IActionResult> ByEmail(string friendEmail)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var friendship = this.db.Friendships.Where(u => (u.FriendFrom.Email.Equals(user.Email) && u.FriendTo.Email.Equals(friendEmail)))
                                                 .Select(e => new Friendship
                                                 {
                                                     FriendFrom = e.FriendFrom,
                                                     FriendTo = e.FriendTo,
                                                     Messages = e.Messages,
                                                 }).FirstOrDefault();

            var receiverFriendship = this.db.Friendships.Where(u => (u.FriendFrom.Email.Equals(friendEmail) && u.FriendTo.Email.Equals(user.Email)))
                                                 .Select(e => new Friendship
                                                 {
                                                     FriendFrom = e.FriendFrom,
                                                     FriendTo = e.FriendTo,
                                                     Messages = e.Messages,
                                                 }).FirstOrDefault();

            var viewmodel = new FriendshipMessagesViewModel();

            if (receiverFriendship != null)
            {

                foreach (var message in receiverFriendship.Messages)
                {
                    friendship.Messages.Add(message);
                }
            }

            var a = friendship.Messages.OrderBy(e => e.CreatedOn).ToList();

            friendship.Messages = a;

            viewmodel.Friendship = friendship;
            return this.View(viewmodel);
        }
    }
}
