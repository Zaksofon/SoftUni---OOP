
namespace Vehicles
{
    public class Car : Vehicle
    {
        private const double AConConsumption = 0.9;
        public Car(double fuelQuantity, double fuelConsumption) : base(fuelQuantity, fuelConsumption)
        {
        }

        protected override double FuelConsumption => base.FuelConsumption + AConConsumption;

    }
}
