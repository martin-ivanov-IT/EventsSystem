namespace EventsSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EventsSystem.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Server.IIS.Core;

    public class TestController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public TestController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        

        public async Task<IActionResult> Test()
        {
            // var result = await this.UserManager.CreateAsync(
            //       new ApplicationUser
            //       {
            //       Email = "test@email.bg",
            //       UserName = "test",
            //       EmailConfirmed = true,
            //       PhoneNumber = "1234567890",
            //   }, "123456");

            // var result = await this.signInManager.PasswordSignInAsync("test", "123456", true, true);

            var result = await this.roleManager.CreateAsync(new ApplicationRole
            {
                Name = "Admin",
            });

            var user = await this.userManager.GetUserAsync(this.User);

            var claim = await userManager.AddClaimAsync(user, new Claim(ClaimTypes.PostalCode, "1000"));

            var postCode = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PostalCode)?.Value;

            await this.userManager.AddToRoleAsync(user, "Admin");

            return this.Json(result);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult WhoAmI()
        {
            return this.Ok(this.User.Identity.Name);
        }
    }
}
