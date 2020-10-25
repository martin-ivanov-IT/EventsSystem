namespace EventsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EventsSystem.Data;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Data;
    using EventsSystem.Web.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CreateEventController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CreateEventController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.Db = db;
            this.userManager = userManager;
        }

        public ApplicationDbContext Db { get; }

        [Authorize]
        public IActionResult FillForm()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
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
                PlaceUrl = "p/" + input.Name.Replace(' ', '-'),
                Initiator = user,
            };

            if (this.Db.Places.Any(p => p.Name.Equals(input.PlaceName) && p.City.Equals(input.PlaceCity) && p.Address.Equals(input.PlaceCity)))
            {
                ev.Place = this.Db.Places.FirstOrDefault(p => p.Name.Equals(input.PlaceName));
            }
            else
            {
                ev.Place = new Place
                {
                    Address = input.PlaceCity,
                    Name = input.PlaceName,
                    City = input.PlaceCity,
                };
            }

            var expectedFileExt = new[] { ".jpg", ".jpeg" };
            if (input.Photos != null)
            {
                foreach (var photo in input.Photos)
                {
                    if (expectedFileExt.Any(x => photo.FileName.EndsWith(x)))
                    {
                        var fileFolder = $"wwwroot\\images\\{input.PlaceName}";
                        System.IO.Directory.CreateDirectory(fileFolder);
                        var path = $"{fileFolder}\\{photo.FileName}";
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await photo.CopyToAsync(fileStream);
                        }

                        ev.Place.Images.Add(new Image
                        {
                            Path = $"images\\{input.PlaceName}\\{photo.FileName}",
                        });
                    }
                }
            }

            this.Db.Events.Add(ev);
            this.Db.SaveChanges();
            return this.Redirect("/");
        }

    }
}
