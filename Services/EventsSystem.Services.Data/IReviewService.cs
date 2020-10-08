using EventsSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Services.Data
{
    public interface IReviewService
    {
        Place GetPlaceByName(string name);
    }
}
