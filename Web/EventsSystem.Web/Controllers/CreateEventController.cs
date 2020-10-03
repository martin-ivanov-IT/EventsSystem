using EventsSystem.Data;
using EventsSystem.Data.Models;
using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.CreateEvent;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class CreateEventController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CreateEventController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.Db = db;
            this.userManager = userManager;
        }

        public ApplicationDbContext Db { get; }

        public IActionResult FillForm()
        {

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> FillForm(CreateEventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var ev = new Event
            {
                Name = input.Name,
                Time = input.Time,
                EntranceFee = input.EntranceFee,
                EntranceType = input.EntranceType,
                Description = input.Description,
                Place = new Place
                {
                    Address = input.PlaceCity,
                },

                Initiator = user,
            };

            this.Db.Events.Add(ev);
            this.Db.SaveChanges();
            return this.Redirect("/");
        }

    }
}
