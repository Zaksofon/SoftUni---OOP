using System;

namespace _02.VehiclesExtension
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Car car = null;
            Truck truck = null;
            Bus bus = null;

            for (int i = 0; i < 3; i++)
            {
                string[] info = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string vehicleType = info[0];
                double fuelQuantity = Convert.ToDouble(info[1]);
                double fuelConsumption = Convert.ToDouble(info[2]);
                double tankCapacity = Convert.ToDouble(info[3]);

                switch (vehicleType)
                {
                    case "Car": car = new Car(fuelQuantity, fuelConsumption, tankCapacity); break;
                    case "Truck": truck = new Truck(fuelQuantity, fuelConsumption, tankCapacity); break;
                    case "Bus": bus = new Bus(fuelQuantity, fuelConsumption, tankCapacity); break;
                }
            }

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] commands = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string command = commands[0];
                string vehicleType = commands[1];
                double distanceOrLiters = double.Parse(commands[2]);

                switch (command)
                {
                    case "DriveEmpty":
                        bus.IsEmpty = true;
                        CanDrive(bus, distanceOrLiters); break;

                    case "Drive":
                        switch (vehicleType)
                        {
                            case "Car":
                                CanDrive(car, distanceOrLiters); break;

                            case "Truck": 
                                CanDrive(truck, distanceOrLiters); break;

                            case "Bus": 
                                bus.IsEmpty = false; 
                                CanDrive(bus, distanceOrLiters); break;
                        }
                        break;

                    case "Refuel":
                        try
                        {
                            switch (vehicleType)
                            {
                                case "Car":
                                    car.Refuel(distanceOrLiters); break;

                                case "Truck":
                                    truck.Refuel(distanceOrLiters); break;

                                case "Bus":
                                    bus.Refuel(distanceOrLiters); break;
                            }
                            break;
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }
                }
            }
            Console.WriteLine($"Car: {car.FuelQuantity:F2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
            Console.WriteLine($"Bus: {bus.FuelQuantity:F2}");
        }

        public static void CanDrive(Vehicle vehicle, double distance)
        {
            bool canDrive = vehicle.CanDrive(distance);
            string vehicleType = vehicle.GetType().Name;

            string result = canDrive
                ? $"{vehicleType} travelled {distance} km"
                : $"{vehicleType} needs refueling";

            Console.WriteLine(result);
        }
    }
}
