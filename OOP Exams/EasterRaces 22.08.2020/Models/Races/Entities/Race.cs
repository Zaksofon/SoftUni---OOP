
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private const int nameMinLength = 5;
        private const int lapsMinCount = 1;
        private IDictionary<string, IDriver> driversByName;
        private string name;
        private int laps;

        public Race(string name, int laps)
        {
            this.name = name;
            this.laps = laps;
            driversByName = new Dictionary<string, IDriver>();
        }

        public string Name
        {
            get => name;
            private set
            {
                Validator.ThrowIfStringIsNullOrWhiteSpaceOrLessThan(value, nameMinLength, string.Format(ExceptionMessages.InvalidName, value, nameMinLength));
                name = value;
            }
        }

        public int Laps
        {
            get => laps;
            private set
            {
                if (value < lapsMinCount)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, value));
                }

                laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => driversByName.Values.ToList();

        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(ExceptionMessages.DriverInvalid);
            }

            if (driver.CanParticipate == false)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.DriverNotParticipate, driver.Name));
            }

            if (driversByName.ContainsKey(driver.Name))
            {
                throw new ArgumentNullException(String.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, Name));
            }
            
            driversByName.Add(driver.Name, driver);
        }
    }
}
