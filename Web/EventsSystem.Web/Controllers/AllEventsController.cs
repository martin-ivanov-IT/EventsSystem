using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class AllEventsController : Controller
    {
        private const int ItemsPerPage = 5;

        public IEventsService EventsService { get; }

        public AllEventsController(IEventsService eventsService)
        {
            this.EventsService = eventsService;
        }

        public IActionResult ShowAllEvents(int page)
        {
            var count = this.EventsService.GetCount();

            var viewModel = new ViewModels.Home.IndexViewModel();
            var events = this.EventsService.GetAll<IndexEventViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);
            foreach (var ev in events)
            {
                ev.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
                
                // maybe wrong
                ev.CurrentPage = page;
            }

            viewModel.Events = events;

            return this.View(viewModel);
        }
    }
}
