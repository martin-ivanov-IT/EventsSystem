using EventsSystem.Data;
using EventsSystem.Data.Models;
using EventsSystem.Services.Data;
using EventsSystem.Web.ViewModels.CreateEvent;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class CreateEventController : Controller
    {
        public CreateEventController(ApplicationDbContext db)
        {
            this.Db = db;
        }

        public ApplicationDbContext Db { get; }

        public IActionResult FillForm()
        {

            return this.View();
        }

        [HttpPost]
        public IActionResult FillForm(CreateEventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var ev = new Event
            {
                Name = input.Name,
                Time = input.Time,
                VisitorsCount = (int)input.VisitorsCount,
                EntranceFee = input.EntranceFee,
                EntranceType = input.EntranceType,
            };

            this.Db.Events.Add(ev);
            Db.SaveChanges();
            return this.Redirect("/");
        }

    }
}
