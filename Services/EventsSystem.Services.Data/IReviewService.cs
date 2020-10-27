namespace EventsSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using EventsSystem.Data.Models;

    public interface IReviewService
    {
        Place GetPlaceByName(string name);
    }
}
