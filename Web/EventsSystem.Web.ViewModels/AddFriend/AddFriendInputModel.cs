namespace EventsSystem.Web.ViewModels.AddFriend
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using EventsSystem.Data.Models;
    using EventsSystem.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class AddFriendInputModel 
    {
        [Required]
        [Display(Name = "Enter the username of your friend")]
        public string Username { get; set; }
    }
}
