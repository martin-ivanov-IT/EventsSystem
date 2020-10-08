using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Services.Data
{
    public interface IEventsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(int id);
    }
}
