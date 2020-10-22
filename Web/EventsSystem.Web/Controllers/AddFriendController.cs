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
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var email = input.Email;
            var friend = this.userManager.Users.FirstOrDefault(u => u.Email == email);
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
