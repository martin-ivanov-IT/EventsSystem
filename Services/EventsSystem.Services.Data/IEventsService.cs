﻿namespace EventsSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IEventsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetAllByCity<T>(string city, int? take = null, int skip = 0);

        T GetByName<T>(string name);

        T GetById<T>(int id);

        int GetCount();

        int GetCountAllUpcomingEvents();

        int GetCountAllUpcomingEventsByCity(string city);

        IEnumerable<T> GetAllByPlaceId<T>(int placeId);
    }
}
