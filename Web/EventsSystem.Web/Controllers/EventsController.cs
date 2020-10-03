using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.Events;
using EventsSystem.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IEventsService eventsService;

        public EventsController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        public IActionResult EventsByName(string name)
        {
            var viewModel = this.eventsService.GetByName<IndexEventViewModel>(name);
            return this.View(viewModel);
        }

    }
}
