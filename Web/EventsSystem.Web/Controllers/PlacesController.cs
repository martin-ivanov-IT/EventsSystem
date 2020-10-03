using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.Places;
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

        public PlacesController(IPlacesService placesService)
        {
            this.placesService = placesService;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.placesService.GetByName<PlaceViewModel>(name);
            return this.View(viewModel);
        }

    }
}
