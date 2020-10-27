namespace EventsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventsSystem.Data;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Data;
    using EventsSystem.Web.ViewModels.Review;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewController : Controller
    {
        private readonly IPlacesService placesService;
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(IPlacesService placesService, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.placesService = placesService;
            this.db = db;
            this.userManager = userManager;
        }

       
        public IActionResult AddReviewToPlace()
        {
           
            return View();

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReviewToPlace(string name, AddReviewToPlaceModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            Place place = this.placesService.GetPlaceByName(name);


            var pl = db.Places.First(a => a.Name.Equals(name));
            var review = new PlaceReview {
                Name = input.Name,
                Description = input.Description,
                User = user,
            };
            pl.Reviews.Add(review);
            this.db.SaveChanges();
            return this.Redirect($"/place/{pl.Id}");

        }
    }
}
