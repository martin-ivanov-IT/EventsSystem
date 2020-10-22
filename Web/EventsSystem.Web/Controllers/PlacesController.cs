using EventsSystem.Data.Models;
using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.Places;
using Microsoft.AspNetCore.Authorization;
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
        private const int ItemsPerPage = 5;

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

        public async Task<IActionResult> TopPlaces()
        {
            bool isAuthenticated = this.User.Identity.IsAuthenticated;
            var viewModel = new PlaceAllViewModel();
            if (isAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                string city = user.City;
                var places = this.placesService.GetAllByCity<PlaceViewModel>(city, 5, 0);
                viewModel.PlacesByCity = places;
            }

            var allPlaces = this.placesService.GetAll<PlaceViewModel>(5, 0);
            viewModel.AllPlaces = allPlaces;


            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> ShowAllPlacesByCity(int page)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string city = user.City;
            var count = this.placesService.GetCountAllPlacesByCity(city);

            var viewModel = new PlaceAllViewModel();
            
            var places = this.placesService.GetAllByCity<PlaceViewModel>(city,ItemsPerPage, (page - 1) * ItemsPerPage);
            foreach (var place in places)
            {
                place.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

                place.CurrentPage = page;
            }

            viewModel.PlacesByCity = places;

            return this.View(viewModel);
        }

        public IActionResult ShowAllPlaces(int page)
        {
            var count = this.placesService.GetCount();

            var viewModel = new PlaceAllViewModel();
            var places = this.placesService.GetAll<PlaceViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);
            foreach (var place in places)
            {
                place.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

                place.CurrentPage = page;
            }

            viewModel.AllPlaces = places;

            return this.View(viewModel);
        }
    }
}
