namespace EventsSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using EventsSystem.Data.Common.Models;

    public class Friendship : BaseDeletableModel<int>
    {
        public Friendship()
        {
            this.Messages = new HashSet<Message>();
        }

        public int Id { get; set; }

        public string FriendFromID { get; set; }

        public string FriendToID { get; set; }

        [ForeignKey("FriendFromID")]
        public virtual ApplicationUser FriendFrom { get; set; }

        [ForeignKey("FriendToID")]
        public virtual ApplicationUser FriendTo { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

    }
}
