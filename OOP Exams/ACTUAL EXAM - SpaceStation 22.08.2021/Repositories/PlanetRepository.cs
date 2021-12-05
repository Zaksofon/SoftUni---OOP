﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;

        public PlanetRepository()
        {
            planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => planets.ToList();

        public void Add(IPlanet model) => planets.Add(model);

        public bool Remove(IPlanet model) => planets.Remove(model);

        public IPlanet FindByName(string name) => planets.FirstOrDefault(n => n.Name == name);

    }
}

