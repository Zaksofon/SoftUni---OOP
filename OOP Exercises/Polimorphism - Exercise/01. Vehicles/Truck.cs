
using System;

namespace Vehicles
{
    public class Truck : Vehicle
    {
        private const double AConConsumption = 1.6;
        public Truck(double fuelQuantity, double fuelConsumption) : base(fuelQuantity, fuelConsumption)
        {
        }

        protected override double FuelConsumption => base.FuelConsumption + AConConsumption;

        public override void Refuel(double amount)
        {
            base.Refuel(amount * 0.95);
        }
    }
}
