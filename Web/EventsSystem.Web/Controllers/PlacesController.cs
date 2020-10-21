using EventsSystem.Data.Models;
using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.Places;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IPlacesService placesService;
        private readonly UserManager<ApplicationUser> userManager;

        public PlacesController(IPlacesService placesService, UserManager<ApplicationUser> userManager)
        {
            this.placesService = placesService;
            this.userManager = userManager;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.placesService.GetByName<PlaceViewModel>(name);
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var postViewModel = this.placesService.GetById<PlaceViewModel>(id);
            if (postViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(postViewModel);
        }

        public async Task<IActionResult> All()
        {
            bool isAuthenticated = this.User.Identity.IsAuthenticated;
            var viewModel = new PlaceAllViewModel();
            if (isAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                string city = user.City;
                var places = this.placesService.GetAllByCity<PlaceViewModel>(city);
                viewModel.Places = places;
            }
            else
            {
            var places = this.placesService.GetAll<PlaceViewModel>();
            viewModel.Places = places;
            }

            return this.View(viewModel);
        }
    }
}
