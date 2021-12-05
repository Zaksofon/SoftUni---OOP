
using System;

namespace Vehicles
{
    public abstract class Vehicle
    {
        protected Vehicle(double fuelQuantity, double fuelConsumption)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
        }
        public double FuelQuantity { get; private set; }
        protected virtual double FuelConsumption { get;}

        public virtual void Refuel(double amount)
        {
            FuelQuantity += amount;
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
