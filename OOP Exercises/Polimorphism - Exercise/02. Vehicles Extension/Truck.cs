
using System;

namespace _02.VehiclesExtension
{
    public class Truck : Vehicle
    {
        private const double AConConsumption = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override double FuelConsumption => base.FuelConsumption + AConConsumption;

        public override void Refuel(double amount)
        {
            if (base.FuelQuantity + amount > this.TankCapacity)
            {
                throw new InvalidOperationException($"Cannot fit {amount} fuel in the tank");
            }
            base.Refuel(amount * 0.95);
        }
    }
}