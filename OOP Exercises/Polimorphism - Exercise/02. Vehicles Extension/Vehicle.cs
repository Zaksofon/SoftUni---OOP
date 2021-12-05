using System;
using System.Collections.Generic;

namespace _02.VehiclesExtension
{
    public abstract class Vehicle
    {
        private double fuelQuantity;
        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            FuelConsumption = fuelConsumption;
            FuelQuantity = fuelQuantity;
            TankCapacity = tankCapacity;
        }

        public double TankCapacity { get; }

        public double FuelQuantity
        {
            get => fuelQuantity;
            private set
            {
                if (this.fuelQuantity > TankCapacity)
                {
                    fuelQuantity = 0;
                }
                else
                {
                    fuelQuantity = value;
                }
            }
        }

        public virtual double FuelConsumption { get; }

        public virtual void Refuel(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Fuel must be a positive number");
            }
            if (amount > TankCapacity)
            {
                throw new InvalidOperationException($"Cannot fit {amount} fuel in the tank");
            }
            else
            {
                FuelQuantity += amount;
            }
        }

        public bool CanDrive(double distance)
        {
            bool canDrive = FuelQuantity - FuelConsumption * distance >= 0;

            if (canDrive is false)
            {
                return false;
            }

            FuelQuantity -= FuelConsumption * distance;
            return true;
        }
    }
}