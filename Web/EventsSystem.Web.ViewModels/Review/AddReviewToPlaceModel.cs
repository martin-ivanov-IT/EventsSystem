using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsSystem.Web.ViewModels.Review
{
    public class AddReviewToPlaceModel
    {
        public string Name { get; set; }

        [Required]
        [Display(Name = "Write your review")]
        public string Description { get; set; }
    }
}
