
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private readonly AstronautRepository astronauts = new AstronautRepository();
        private readonly PlanetRepository planets = new PlanetRepository();
        private int missionsCounter = 0;

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut = null;

            switch (type)
            {
                case nameof(Biologist):
                    astronaut = new Biologist(astronautName); break;

                case nameof(Geodesist):
                    astronaut = new Geodesist(astronautName); break;

                case nameof(Meteorologist):
                    astronaut = new Meteorologist(astronautName); break;

                default: throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            astronauts.Add(astronaut);
            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);

            foreach (var item in items) planet.Items.Add(item);
            planets.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronautToRetire = astronauts.FindByName(astronautName);
            if (astronautToRetire == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }

            astronauts.Remove(astronautToRetire);
            return String.Format(OutputMessages.AstronautRetired, astronautName);
        }

        public string ExplorePlanet(string planetName)
        {
            var suitableAstronauts = astronauts
                .Models
                .Where(o => o.Oxygen > 60)
                .ToList();

            if (suitableAstronauts.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IPlanet currentPlanet = planets.FindByName(planetName);

            IMission currentMission = new Mission();
            currentMission.Explore(currentPlanet, new List<IAstronaut>(suitableAstronauts));

            var deadExplorers = suitableAstronauts.Count(o => o.CanBreath == false);
            planets.Remove(currentPlanet);

            missionsCounter++;
            return String.Format(OutputMessages.PlanetExplored, planetName, deadExplorers);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{missionsCounter} planets were explored!");
            sb.AppendLine("Astronauts info:");

            foreach (var astronaut in astronauts.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                sb.AppendLine(astronaut.Bag.Items.Count != 0 ? $"Bag items: {string.Join(", ", astronaut.Bag.Items)}" : "Bag items: none");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
