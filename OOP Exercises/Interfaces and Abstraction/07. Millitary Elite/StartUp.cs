using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryElite.Classes;
using MilitaryElite.Enumerators;
using MilitaryElite.Interfaces;

namespace MilitaryElite
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<IPrivate> privates = new List<IPrivate>();
            List<ISoldier> soldiers = new List<ISoldier>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "End")
                {
                    break;
                }

                string[] parts = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string unit = parts[0];
                int id = int.Parse(parts[1]);
                string firstName = parts[2];
                string secondName = parts[3];
                decimal salary = Convert.ToDecimal(parts[4]);
                Corps corps = default;
                bool isCorpValid = default;

                switch (unit)
                {
                    case "Private":
                        IPrivate @private = new Private(id, firstName, secondName, salary);
                        privates.Add(@private);
                        soldiers.Add(@private);
                        break;

                    case "LieutenantGeneral":
                        ILieutenantGeneral lieutenantGeneral = new LieutenantGeneral(id, firstName, secondName, salary);

                        for (int i = 5; i < parts.Length; i++)
                        {
                            int privateId = int.Parse(parts[i]);

                            if (privates.Any(n => n.Id == privateId))
                            {
                                lieutenantGeneral.AddPrivate(privates.First(n => n.Id == privateId));
                            }
                        } 
                        soldiers.Add(lieutenantGeneral);
                        break;

                    case "Engineer":
                        isCorpValid = Enum.TryParse(parts[5], out corps);

                        if (!isCorpValid)
                        {
                            continue;
                        }

                        IEngineer engineer = new Engineer(id, firstName, secondName, salary, corps);

                        for (int i = 6; i < parts.Length; i += 2)
                        {
                            string repairPart = parts[i];
                            int hoursWorked = int.Parse(parts[i + 1]);

                            IRepair repair = new Repair(repairPart, hoursWorked);
                            engineer.AddRepairs(repair);
                        }
                        soldiers.Add(engineer);
                        break;

                    case "Commando":
                        isCorpValid = Enum.TryParse(parts[5], out corps);

                        if (!isCorpValid)
                        {
                            continue;
                        }

                        ICommando commando = new Commando(id, firstName, secondName, salary, corps);

                        for (int i = 6; i < parts.Length; i+=2)
                        {
                            string missionName = parts[i];
                            string missionState = parts[i + 1];

                            bool isMissionValid = Enum.TryParse(missionState, out MissionState state);

                            if (!isMissionValid)
                            {
                                continue;
                            }

                            IMission mission = new Mission(missionName, state);
                            commando.AddMission(mission);
                        }
                        soldiers.Add(commando);
                        break;

                    case "Spy":
                        int codeNumber = int.Parse(parts[4]);
                        ISpy spy = new Spy(id, firstName, secondName, codeNumber);
                        soldiers.Add(spy);
                        break;
                }
            }

            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier);
            }
        }
    }
}
