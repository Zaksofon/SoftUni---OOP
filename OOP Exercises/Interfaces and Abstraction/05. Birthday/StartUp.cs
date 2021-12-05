using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BirthdayCelebrations
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<IBirthable> dates = new List<IBirthable>();
            List<IIdenable> ids = new List<IIdenable>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "End")
                {
                    string yearToCheck = Console.ReadLine();
                    foreach (var date in dates)
                    {
                        if (date.Birthdate.EndsWith(yearToCheck))
                        {
                            Console.WriteLine(date.ToString());
                        }
                    }
                    break;
                }

                string[] parts = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string creatureType = parts[0];
                string name = parts[1];
                string id = default;
                string birthdate = default;

                switch (creatureType)
                {
                    case "Citizen":
                        int age = int.Parse(parts[2]);
                        id = parts[3];
                        birthdate = parts[4];

                        dates.Add(new Citizens(name, age, id, birthdate));
                        ids.Add(new Citizens(name, age, id));
                        break;

                    case "Pet":
                        birthdate = parts[2];

                        dates.Add(new Pets(name, birthdate));
                        break;

                    case "Robot":
                        string model = name;
                        id = parts[2];

                        ids.Add(new Robots(name, id));
                        break;
                }
            }
        }
    }
}
