
using System;
using System.Linq;
using EasterRaces.Core.Contracts;
using EasterRaces.Models;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private readonly IRepository<IDriver> driversRepository;
        private readonly IRepository<ICar> carsRepository;
        private readonly IRepository<IRace> racesRepository;

        public ChampionshipController()
        {
            this.driversRepository = new DriverRepository();
            this.carsRepository = new CarRepository();
            this.racesRepository = new RaceRepository();
        }

        public  string CreateDriver(string driverName)
        {
            IDriver driver = new Driver(driverName);

            driversRepository.Add(driver);

            return String.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            type = type + "Car";

            ICar car = null;

            switch (type)
            {
                case nameof(MuscleCar):
                    car = new MuscleCar(model, horsePower); break;

                case nameof(SportsCar):
                    car = new SportsCar(model, horsePower); break;
            }
            carsRepository.Add(car);
            return String.Format(OutputMessages.CarCreated, type, model);
        }

        public string CreateRace(string name, int laps)
        {
            IRace race = new Race(name, laps);
            racesRepository.Add(race);
            return String.Format(OutputMessages.RaceCreated, name);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = racesRepository.GetByName(raceName);
            IDriver driver = driversRepository.GetByName(driverName);

            if (race == null)
            {
                return String.Format(ExceptionMessages.RaceNotFound, raceName);
            }

            if (driver == null)
            {
                return String.Format(ExceptionMessages.DriverNotFound, driverName);
            }

            race.AddDriver(driver);
            return String.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            ICar car = carsRepository.GetByName(carModel);
            IDriver driver = driversRepository.GetByName(driverName);

            if (car == null)
            {
                return String.Format(ExceptionMessages.CarNotFound, carModel);
            }

            if (driver == null)
            {
                return String.Format(ExceptionMessages.DriverNotFound, driverName);
            }

            driver.AddCar(car);
            return String.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string StartRace(string raceName)
        {
            IRace race = racesRepository.GetByName(raceName);

            if (race == null)
            {
                return String.Format(ExceptionMessages.RaceNotFound, raceName);
            }

            if (race.Drivers.Count < 3)
            {
                return String.Format(ExceptionMessages.RaceInvalid, raceName, 3);
            }

            var winners = race.Drivers.OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps))
                .Take(3)
                .ToArray();

           racesRepository.Remove(race);

           return String.Format(OutputMessages.DriverFirstPosition, winners[0].Name, race.Name) + Environment.NewLine
              + string.Format(OutputMessages.DriverSecondPosition, winners[1].Name, race.Name) + Environment.NewLine
              + String.Format(OutputMessages.DriverThirdPosition, winners[2].Name, race.Name);
           
        }
    }
}
