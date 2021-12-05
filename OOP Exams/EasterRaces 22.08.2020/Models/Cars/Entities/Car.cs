
using System;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models
{
    public abstract class Car : ICar
    {
        private const int modelMinSymbolsLength = 4;
        private string model;
        private int horsePower;
        private int minHorsePower;
        private int maxHorsePower;

        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;

            this.Model = model;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
        }

        public string Model
        {
            get => model;
            private set
            {
                Validator.ThrowIfStringIsNullOrWhiteSpaceOrLessThan(value, modelMinSymbolsLength, String.Format(ExceptionMessages.InvalidModel, value, modelMinSymbolsLength));

                model = value;
            }
        }

        public int HorsePower
        {
            get => horsePower;
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidHorsePower, value));
                }

                horsePower = value;
            }
        }

        public double CubicCentimeters { get; }
        
        public double CalculateRacePoints(int laps)
        {
            return CubicCentimeters / HorsePower * laps;
        }
    }
}
