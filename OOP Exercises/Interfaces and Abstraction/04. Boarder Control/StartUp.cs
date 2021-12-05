using System;
using System.Collections.Generic;

namespace BorderControl
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<string> ids = new List<string>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "End")
                {
                    string codeNumber = Console.ReadLine();

                    foreach (var id in ids)
                    {
                        if (id.EndsWith(codeNumber))
                        {
                            Console.WriteLine(id);
                        }
                    }
                    break;
                }

                string[] parts = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                switch (parts.Length)
                {
                    case 3:
                    {
                        string name = parts[0];
                        int age = int.Parse(parts[1]);
                        string humanId = parts[2];
                        ids.Add(humanId);
                        Citizens citizen = new Citizens(name, age, humanId);
                        break;
                    }
                    case 2:
                    {
                        string model = parts[0];
                        string robotId = parts[1];
                        ids.Add(robotId);
                        Robots robot = new Robots(model, robotId);
                        break;
                    }
                }
            }
        }
    }
}
