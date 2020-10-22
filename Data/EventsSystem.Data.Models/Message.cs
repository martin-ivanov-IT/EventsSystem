using EventsSystem.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventsSystem.Data.Models
{
    public class Message : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int FriendshipID { get; set; }

        public string UserID { get; set; }

        [ForeignKey("FriendshipID")]
        public virtual Friendship Friendship { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}
