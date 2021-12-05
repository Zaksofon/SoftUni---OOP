
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Models.Workshops.Contracts;
using Easter.Repositories;
using Easter.Utilities.Messages;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnies;
        private EggRepository eggs;

        public Controller()
        {
            this.bunnies = new BunnyRepository();
            this.eggs = new EggRepository();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            Bunny newBunny = null;

            switch (bunnyType)
            {
                case nameof(HappyBunny):
                    newBunny = new HappyBunny(bunnyName); break;

                case nameof(SleepyBunny):
                    newBunny = new SleepyBunny(bunnyName); break;

                default: throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            bunnies.Add(newBunny);
            return String.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            var bunny = bunnies.FindByName(bunnyName);

            if (bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            bunny.AddDye(new Dye(power));
            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            Egg newEgg = new Egg(eggName, energyRequired);
            eggs.Add(newEgg);
            return String.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);
            IWorkshop workshop = new Workshop();

            List<IBunny> suitableBunnies = bunnies.Models
                .Where(x => x.Energy >= 50)
                .OrderByDescending(x => x.Energy)
                .ToList();

            if (suitableBunnies.Any() == false)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            while (suitableBunnies.Any())
            {
                IBunny currentBunny = suitableBunnies.First();

                while (true)
                {
                    if (currentBunny.Energy == 0 || currentBunny.Dyes.All(x => x.IsFinished()))
                    {
                        suitableBunnies.Remove(currentBunny);
                        break;
                    }

                    workshop.Color(egg, currentBunny);

                    if (egg.IsDone())
                    {
                        break;
                    }
                }

                if (egg.IsDone())
                {
                    break;
                }
            }

            return $"Egg {eggName} is {(egg.IsDone() ? "done" : "not done")}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{eggs.Models.Count(x => x.IsDone())} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (IBunny bunny in bunnies.Models)
            {
                sb.AppendLine($"Name: {bunny.Name}");
                sb.AppendLine($"Energy: {bunny.Energy}");
                sb.AppendLine($"Dyes: {bunny.Dyes.Count(x => !x.IsFinished())} not finished");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
