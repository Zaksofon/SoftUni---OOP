using System;
using System.Collections.Generic;
using System.Linq;
using FoodShortage.Classes;
using FoodShortage.Interfaces;

namespace FoodShortage
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<IBuyer> citizens = new List<IBuyer>();
            List<IBuyer> rebels = new List<IBuyer>();

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = input[0];
                int age = int.Parse(input[1]);

                if (input.Length == 3)
                {
                    string group = input[2];

                    Rebel rebel = new Rebel(name, age, group, 0);
                    rebels.Add(rebel);
                }

                else if (input.Length == 4)
                {
                    string id = input[2];
                    string birthDate = input[3];

                    Citizen citizen = new Citizen(name, age, id, birthDate, 0);
                    citizens.Add(citizen);
                }
            }

            while (true)
            {
                string inputName = Console.ReadLine();

                if (inputName == "End")
                {
                    break;
                }

                if (citizens.Any(n => n.Name == inputName))
                {
                    citizens.First(n => n.Name == inputName).BuyFood();
                }

                else if (rebels.Any(n => n.Name == inputName))
                {
                    rebels.First(n => n.Name == inputName).BuyFood();
                }
            }

            int result = citizens.Sum(n => n.Food) + rebels.Sum(n => n.Food);
            Console.WriteLine(result);
        }
    }
}
