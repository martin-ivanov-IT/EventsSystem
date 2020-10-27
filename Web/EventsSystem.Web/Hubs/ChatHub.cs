using EventsSystem.Data;
using EventsSystem.Data.Models;
using EventsSystem.Web.ViewModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public ChatHub(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public async Task Send(string[] paramenters)
        {
            string userEmail = paramenters[0];
            string friendEmail = paramenters[1];
            string messageInput = paramenters[2];
            var userEmailData = this.Context.User.Identity.Name;
            var friend = this.userManager.Users.FirstOrDefault(u => u.Email == friendEmail);
            var user = this.userManager.Users.FirstOrDefault(u => u.Email == userEmail);

            var friendship = this.db.Friendships
                                   .FirstOrDefault(f => (f.FriendFrom.Email.Equals(userEmail) && f.FriendTo.Email.Equals(friendEmail)));

            var friendshipReceiver = this.db.Friendships
                                      .FirstOrDefault(f => (f.FriendFrom.Email.Equals(friendEmail) && f.FriendTo.Email.Equals(userEmail)));

            var message = new Message
            {
                Content = messageInput,
                User = user,
            };

            friendship.Messages.Add(message);
            friendshipReceiver.Messages.Add(message);

            this.db.SaveChanges();

            await this.Clients.Users(user.Id, friend.Id).SendAsync(
                "NewMessage",
                new MessageModel { User = this.Context.User.Identity.Name, Text = $"{user.UserName}: {messageInput}" });
        }
    }
}
