namespace EventsSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using EventsSystem.Data;
    using EventsSystem.Data.Common.Repositories;
    using EventsSystem.Data.Models;
    using EventsSystem.Web.ViewModels;
    using EventsSystem.Web.ViewModels.Administration.Dashboard;
    using EventsSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;
    using EventsSystem.Services.Mapping;
    using EventsSystem.Services.Data;

    public class HomeController : BaseController
    {
        public IPlacesService placesService { get; }

        public HomeController(IPlacesService placesService)
        {
            this.placesService = placesService;
        }

        public IActionResult Index()
        {

            var viewModel = new ViewModels.Home.IndexViewModel();
            var places = this.placesService.GetAll<IndexPlaceViewModel>();

            viewModel.Places = places;

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
