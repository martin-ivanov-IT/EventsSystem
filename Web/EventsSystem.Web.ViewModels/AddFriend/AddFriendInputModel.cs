using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsSystem.Web.ViewModels.AddFriend
{
    public class AddFriendInputModel 
    {
        [Required]
        [Display(Name = "Enter the email of your friend")]
        public string Email { get; set; }
    }
}
