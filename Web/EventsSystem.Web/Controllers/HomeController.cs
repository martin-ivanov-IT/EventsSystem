namespace EventsSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using EventsSystem.Data;
    using EventsSystem.Data.Common.Repositories;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Data;
    using EventsSystem.Services.Mapping;
    using EventsSystem.Web.ViewModels;
    using EventsSystem.Web.ViewModels.Administration.Dashboard;
    using EventsSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
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
