﻿using EventsSystem.Data.Models;
using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.Events;
using EventsSystem.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;
        private const int ItemsPerPage = 5;

        public EventsController(IEventsService eventsService, UserManager<ApplicationUser> userManager)
        {
            this.eventsService = eventsService;
            this.userManager = userManager;
        }

        public IActionResult EventsByName(string name)
        {
            var viewModel = this.eventsService.GetByName<EventViewModel>(name);
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var postViewModel = this.eventsService.GetById<EventViewModel>(id);
            if (postViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(postViewModel);
        }

        public IActionResult ShowAllEvents(int page)
        {
            var count = this.eventsService.GetCount();

            var viewModel = new ViewModels.Home.IndexViewModel();
            var events = this.eventsService.GetAll<IndexEventViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);
            foreach (var ev in events)
            {
                ev.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

                ev.CurrentPage = page;
            }

            viewModel.Events = events;

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> ShowAllEventsByCity(int page)
        {
            var count = this.eventsService.GetCount();

            var viewModel = new ViewModels.Home.IndexViewModel();
            var user = await this.userManager.GetUserAsync(this.User);
            string city = user.City;
            var eventsByCity = this.eventsService.GetAllByCity<IndexEventViewModel>(city, 5, 0);
            viewModel.EventsByCity = eventsByCity;

            // var events = this.eventsService.GetAll<IndexEventViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);
            foreach (var ev in eventsByCity)
            {
                ev.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

                ev.CurrentPage = page;
            }

            viewModel.EventsByCity = eventsByCity;

            return this.View(viewModel);
        }

        
    }
}
