using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.AllFreinds
{
   public class FriendViewModel : IMapFrom<Friendship>
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
