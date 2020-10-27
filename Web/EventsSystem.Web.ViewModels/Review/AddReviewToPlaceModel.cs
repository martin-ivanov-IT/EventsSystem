namespace EventsSystem.Web.ViewModels.Review
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AddReviewToPlaceModel
    {
        public string Name { get; set; }

        [Required]
        [Display(Name = "Write your review")]
        public string Description { get; set; }
    }
}
