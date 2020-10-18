namespace EventsSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EventsSystem.Data;
    using EventsSystem.Data.Common.Repositories;
    using EventsSystem.Data.Models;
    using EventsSystem.Services.Mapping;
   

    public class PlacesService : IPlacesService
    {
        private readonly IDeletableEntityRepository<Place> placesRepository;

        public PlacesService(IDeletableEntityRepository<Place> placesRepository)
        {
            this.placesRepository = placesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Place> query = this.placesRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var place = this.placesRepository.All().Where(x => x.Name == name.Replace("-", " "))
                   .To<T>().FirstOrDefault();
            return place;
        }

        public Place GetPlaceByName(string name)
        {
            IQueryable<Place> places = this.placesRepository.All();

            return places.FirstOrDefault(x => x.Name.Equals(name.Replace("-", " ")));
        }

        public T GetById<T>(int id)
        {
            var place = this.placesRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return place;
        }

    };
}
