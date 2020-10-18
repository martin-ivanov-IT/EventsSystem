// ReSharper disable VirtualMemberCallInConstructor
namespace EventsSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EventsSystem.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Events = new HashSet<Event>();
            this.FriendTos = new HashSet<Friendship>();
            this.FriendFroms = new HashSet<Friendship>();
            this.Messages = new HashSet<Message>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string City { get; set; }

        public string Surname { get; set; }

        public string Lastname { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<PlaceReview> PlaceReviews { get; set; }

        public virtual ICollection<EventReview> EventReviews { get; set; }

        public virtual ICollection<Friendship> FriendFroms { get; set; }

        public virtual ICollection<Friendship> FriendTos { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

    }
}
