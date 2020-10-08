using EventsSystem.Data;
using EventsSystem.Data.Models;
using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.Review;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IPlacesService placesService;
        private readonly ApplicationDbContext db;


        public ReviewController(IPlacesService placesService, ApplicationDbContext db)
        {
            this.placesService = placesService;
            this.db = db;
        }

       
        public IActionResult AddReviewToPlace()
        {
           
            return View();

        }

        [HttpPost]
        public IActionResult AddReviewToPlace(string name, AddReviewToPlaceModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            Place place = placesService.GetPlaceByName(name); 

           
            var pl = db.Places.First(a => a.Name.Equals(name));
            var review = new PlaceReview {
                Name = input.Name,
                Description = input.Description,
            };
            pl.Reviews.Add(review);
            this.db.SaveChanges();
            return this.Redirect("/");

        }
    }
}
