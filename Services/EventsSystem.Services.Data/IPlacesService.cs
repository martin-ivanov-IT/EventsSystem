namespace EventsSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    using EventsSystem.Data.Models;

    public interface IPlacesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetAllByCity<T>(string city, int? take = null, int skip = 0);

        T GetByName<T>(string name);

        Place GetPlaceByName(string name);

        T GetById<T>(int id);

        int GetCount();

        int GetCountAllPlacesByCity(string city);
    }
}
