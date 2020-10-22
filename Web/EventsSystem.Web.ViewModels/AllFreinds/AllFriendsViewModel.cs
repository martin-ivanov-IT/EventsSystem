using EventsSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.AllFreinds
{
    public class AllFriendsViewModel
    {
        public IEnumerable<FriendViewModel> Friends { get; set; }
    }
}
