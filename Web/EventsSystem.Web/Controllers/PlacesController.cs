namespace EventsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventsSystem.Data.Models;
    using EventsSystem.Services.Data;
    using EventsSystem.Web.ViewModels.Events;
    using EventsSystem.Web.ViewModels.Places;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PlacesController : Controller
    {
        private readonly IPlacesService placesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventsService eventsService;
        private const int ItemsPerPage = 5;

        public PlacesController(
            IPlacesService placesService,
            UserManager<ApplicationUser> userManager,
            IEventsService eventsService)
        {
            this.placesService = placesService;
            this.userManager = userManager;
            this.eventsService = eventsService;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.placesService.GetByName<PlaceViewModel>(name);
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var placeViewModel = this.placesService.GetById<PlaceViewModel>(id);
            if (placeViewModel == null)
            {
                this.RedirectToAction("Error", "Shared");
            }

            var placeWithEventViewModel = new PlaceWithEventViewModel();
            placeWithEventViewModel.Place = placeViewModel;
            placeWithEventViewModel.Events = this.eventsService.GetAllByPlaceId<EventViewModel>(id);

            return this.View(placeWithEventViewModel);
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

            if (places.Count() == 0)
            {
                return this.RedirectToAction("NoElements", "Shared");
            }

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

            if (places.Count() == 0)
            {
                return this.RedirectToAction("NoElements", "Shared");
            }

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
