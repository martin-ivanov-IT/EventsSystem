namespace EventsSystem.Web.ViewModels.AllFreinds
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EventsSystem.Data.Models;
    using EventsSystem.Services.Mapping;

   public class FriendViewModel : IMapFrom<Friendship>
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
