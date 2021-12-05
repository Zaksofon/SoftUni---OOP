using System;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;

namespace Vehicles
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] truckInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int n = int.Parse(Console.ReadLine());

            Vehicle car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]));
            Vehicle truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]));

            for (int i = 0; i < n; i++)
            {
                string[] commands = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string command = commands[0];
                string vehicleType = commands[1];
                double distanceOrLitres = double.Parse(commands[2]);

                switch (command)
                {
                    case "Drive":
                        switch (vehicleType)
                        {
                            case "Car":
                                if (car.CanDrive(distanceOrLitres) == false)
                                {
                                    Console.WriteLine("Car needs refueling");
                                    continue;
                                }
                                break;

                            case "Truck":
                                if (truck.CanDrive(distanceOrLitres) == false)
                                {
                                    Console.WriteLine("Truck needs refueling");
                                    continue;
                                }
                                break;
                        }

                        Console.WriteLine($"{vehicleType} travelled {distanceOrLitres} km");
                        break;

                    case "Refuel":
                        switch (vehicleType)
                        {
                            case "Car":
                                car.Refuel(distanceOrLitres);
                                break;

                            case "Truck":
                                truck.Refuel(distanceOrLitres);
                                break;
                        }
                        break;
                }
            }
            Console.WriteLine($"Car: {car.FuelQuantity:F2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
        }
    }
}
