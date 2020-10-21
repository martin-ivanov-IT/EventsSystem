using EventsSystem.Data.Models;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Services.Data
{
    public interface IPlacesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllByCity<T>(string city, int? count = null);

        T GetByName<T>(string name);

        Place GetPlaceByName(string name);

        T GetById<T>(int id);
    }
}
