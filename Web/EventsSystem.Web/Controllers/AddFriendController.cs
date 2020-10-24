using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsSystem.Web.ViewModels.AddFriend;
using EventsSystem.Data;
using EventsSystem.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace EventsSystem.Web.Controllers
{
    public class AddFriendController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public AddFriendController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IActionResult AddPeopleByEmail()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPeopleByEmail(AddFriendInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var username = input.Username;
            var friend = this.userManager.Users.FirstOrDefault(u => u.UserName == username);

            if (username.Equals(user.UserName))
            {
                this.ModelState.AddModelError("Username", " you can not be added es friend to yourself");
            }
            else if (this.db.Friendships.Any(f => f.FriendFrom.UserName.Equals(username)) ||
                this.db.Friendships.Any(f => f.FriendTo.UserName.Equals(username)))
            {
                this.ModelState.AddModelError("Username", " already friends...");
            }

            if (friend == null)
            {
                this.ModelState.AddModelError("Username", "not found...");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // this.ViewBag.Message = "user not found";

            var friendShip = new Friendship
            {
                FriendFrom = user,
                FriendTo = friend,
            };

            var friendShipReceiver = new Friendship
            {
                FriendFrom = friend,
                FriendTo = user,
            };

            this.db.Friendships.Add(friendShip);
            this.db.Friendships.Add(friendShipReceiver);
            this.db.SaveChanges();

            return this.Redirect("/");
        }
    }
}
