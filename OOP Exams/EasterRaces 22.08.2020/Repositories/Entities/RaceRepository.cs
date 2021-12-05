using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly Dictionary<string, IRace> raceByName;

        public RaceRepository()
        {
            this.raceByName = new Dictionary<string, IRace>();
        }

        public IRace GetByName(string name)
        {
            IRace race = null;

            if (raceByName.ContainsKey(name))
            {
                race = raceByName[name];
            }

            return race;
        }

        public IReadOnlyCollection<IRace> GetAll()
        {
            return raceByName.Values.ToList();
        }

        public void Add(IRace model)
        {
            if (raceByName.ContainsKey(model.Name))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceExists, model.Name));
            }
            
            raceByName.Add(model.Name, model);
        }

        public bool Remove(IRace model)
        {
            return raceByName.Remove(model.Name);
        }
    }
}
