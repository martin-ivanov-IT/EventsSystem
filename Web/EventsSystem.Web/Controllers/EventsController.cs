using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class EventsController : BaseController
    {
        public IActionResult EventsByName()
        {
            return this.View();
        }

    }
}
