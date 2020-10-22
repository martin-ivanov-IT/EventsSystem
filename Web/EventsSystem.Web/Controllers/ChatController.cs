using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsSystem.Web.Controllers
{
    public class ChatController: Controller
    {
        public IActionResult Friends()
        {
            return this.View();
        }
        }
}
