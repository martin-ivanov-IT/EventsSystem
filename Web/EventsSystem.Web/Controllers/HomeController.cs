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
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IEventsService EventsService { get; }

        public HomeController(IEventsService eventsService, UserManager<ApplicationUser> userManager)
        {
            this.EventsService = eventsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string name)
        {
            bool isAuthenticated = this.User.Identity.IsAuthenticated;
            var viewModel = new ViewModels.Home.IndexViewModel();
            if (isAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                string city = user.City;
                var eventsByCity = this.EventsService.GetAllByCity<IndexEventViewModel>(city, 5, 0);
                viewModel.EventsByCity = eventsByCity;
            }

            var events = this.EventsService.GetAll<IndexEventViewModel>(5, 0);
            viewModel.Events = events;

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
