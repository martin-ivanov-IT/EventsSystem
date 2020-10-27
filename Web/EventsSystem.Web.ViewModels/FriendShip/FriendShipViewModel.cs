namespace EventsSystem.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EventsSystem.Data.Models;

    public class FriendShipViewModel
    {
        public FriendShipViewModel()
        {
            this.FriendshipsModel = new List<Friendship>();
        }

        public List<Friendship> FriendshipsModel;
    }
}
