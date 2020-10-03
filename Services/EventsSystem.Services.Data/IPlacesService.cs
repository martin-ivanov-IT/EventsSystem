using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Services.Data
{
    public interface IPlacesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
