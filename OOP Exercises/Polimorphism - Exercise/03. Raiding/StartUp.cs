using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.Raiding
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<Hero> heros = new List<Hero>();

            for (int i = 0; i < n; i++)
            {
                string heroName = Console.ReadLine();
                string heroType = Console.ReadLine();

                try
                {
                    switch (heroType)
                    {
                        case "Druid": heros.Add(new Druid(heroName)); break;

                        case "Paladin": heros.Add(new Paladin(heroName)); break;

                        case "Rogue": heros.Add(new Rogue(heroName)); break;

                        case "Warrior": heros.Add(new Warrior(heroName)); break;

                        default: throw new ArgumentException("Invalid hero!"); 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            int bossPower = int.Parse(Console.ReadLine());

            foreach (var hero in heros)
            {
                Console.WriteLine(hero.CastAbility());
            }

            Console.WriteLine(heros.Sum(x => x.Power) >= bossPower ? "Victory!" : "Defeat...");
        }
    }
}
