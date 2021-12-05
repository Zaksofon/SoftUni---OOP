using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private readonly IDictionary<string, ICar> carByModel;

        public CarRepository()
        {
             carByModel = new Dictionary<string, ICar>();
        }

        public ICar GetByName(string name)
        {
            ICar car = null;

            if (carByModel.ContainsKey(name))
            {
                car = carByModel[name];
            }

            return car;
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
            return carByModel.Values.ToList();
        }

        public void Add(ICar model)
        {
            if (carByModel.ContainsKey(model.Model))
            {
                throw new ArgumentException(String.Format(ExceptionMessages.CarExists, model.Model));
            }

            carByModel.Add(model.Model, model);
        }   

        public bool Remove(ICar model)
        {
            return carByModel.Remove(model.Model);
        }
    }
}
