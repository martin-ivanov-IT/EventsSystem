using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult NoElements()
        {
            return this.View();
        }
    }
}
