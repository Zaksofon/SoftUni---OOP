
namespace _02.VehiclesExtension
{
    public class Bus : Vehicle
    {
        private const double AConConsumption = 1.4;
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public bool IsEmpty { get; set; }

        public override double FuelConsumption => IsEmpty
            ? base.FuelConsumption
            : base.FuelConsumption + AConConsumption;
    }
}
