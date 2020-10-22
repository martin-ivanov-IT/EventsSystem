using EventsSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels
{
    public class FriendShipViewModel
    {
        public FriendShipViewModel()
        {
            this.FriendshipsModel = new List<Friendship>();
        }

        public List<Friendship> FriendshipsModel;
    }
}
