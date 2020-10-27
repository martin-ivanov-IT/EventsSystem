namespace EventsSystem.Web.ViewModels.AllFreinds
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EventsSystem.Data.Models;

    public class AllFriendsViewModel
    {
        public IEnumerable<FriendViewModel> Friends { get; set; }
    }
}
