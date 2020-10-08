using EventsSystem.Data.Common.Repositories;
using EventsSystem.Data.Models;
using EventsSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsSystem.Services.Data
{
    public class EventsService : IEventsService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;

        public EventsService (IDeletableEntityRepository<Event> eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Event> query = this.eventsRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var ev = this.eventsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return ev;
        }

        public T GetByName<T>(string name)
        {
            var _event = this.eventsRepository.All().Where(x => x.Name == name.Replace("-", " "))
                   .To<T>().FirstOrDefault();
            return _event;
        }
    }
}
