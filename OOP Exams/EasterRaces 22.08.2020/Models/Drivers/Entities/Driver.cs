
using System;
using System.Collections.Generic;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private const int nameMinLength = 5;
        private string name;

        public Driver(string name)
        {
            this.Name = name;
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
        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate => Car != null;
      
        public void WinRace()
        {
            NumberOfWins++;
        }

        public void AddCar(ICar car)
        {
            Car = car ?? throw new ArgumentNullException(ExceptionMessages.CarInvalid);
        }
    }
}
