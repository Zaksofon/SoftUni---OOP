using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        private readonly Dictionary<string, IDriver> driverByName;

        public DriverRepository()
        {
            this.driverByName = new Dictionary<string, IDriver>();
        }

        public IDriver GetByName(string name)
        {
            IDriver driver = null;

            if (driverByName.ContainsKey(name))
            {
                driver = driverByName[name];
            }

            return driver;
        }

        public IReadOnlyCollection<IDriver> GetAll()
        {
           return driverByName.Values.ToList();
        }

        public void Add(IDriver model)
        {
            if (driverByName.ContainsKey(model.Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, model.Name));
            }
            driverByName.Add(model.Name, model);
        }

        public bool Remove(IDriver model)
        {
           return driverByName.Remove(model.Name);
        }
    }
}
