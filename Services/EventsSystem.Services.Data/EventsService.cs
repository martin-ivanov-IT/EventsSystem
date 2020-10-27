namespace EventsSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using EventsSystem.Data.Common.Repositories;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Mapping;

    public class EventsService : IEventsService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;

        public EventsService (IDeletableEntityRepository<Event> eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Event> query = this.eventsRepository.All().OrderByDescending(x => x.Votes.Count);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            DateTime localDate = DateTime.Now;
            var query = this.eventsRepository.All()
                .Where(e => e.Time.CompareTo(localDate) > 0)
                .OrderByDescending(x => x.Votes.Count)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByCity<T>(string city = null, int? take = null, int skip = 0)
        {
            DateTime localDate = DateTime.Now;
            var query = this.eventsRepository
                .All()
                .Where(e => e.Place.City
                .Equals(city) && e.Time.CompareTo(localDate) > 0)
                .OrderByDescending(x => x.Votes.Count)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByPlaceId<T>(int placeId)
        {
            IQueryable<Event> query = this.eventsRepository.All().Where(e => e.PlaceId == placeId).OrderByDescending(x => x.Votes.Count);

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

        public int GetCount()
        {
            return this.eventsRepository.All().Count();
        }

        public int GetCountAllPlacesByCity(string city)
        {
            return this.eventsRepository.All().Where(e => e.Place.City.Equals(city)).Count();
        }

        public int GetCountAllUpcomingEvents()
        {
            DateTime localDate = DateTime.Now;

            return this.eventsRepository
                .All()
                .Where(e => e.Time.CompareTo(localDate) > 0)
                .Count();
        }

        public int GetCountAllUpcomingEventsByCity(string city)
        {
            DateTime localDate = DateTime.Now;

            return this.eventsRepository
                .All()
                .Where(e => e.Place.City.Equals(city) && e.Time.CompareTo(localDate) > 0)
                .Count();
        }
    }
}
