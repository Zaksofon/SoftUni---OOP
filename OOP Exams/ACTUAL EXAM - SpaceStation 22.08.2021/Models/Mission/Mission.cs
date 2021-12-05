
using System.Collections.Generic;
using System.Linq;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            while (planet.Items.Count != 0)
            {
                var item = planet.Items.First();
                var explorer = astronauts.First(o => o.CanBreath);

                explorer.Breath();
                explorer.Bag.Items.Add(item);
                planet.Items.Remove(item);

                if (explorer.CanBreath == false)
                {
                    astronauts.Remove(explorer);
                }
            }
        }
    }
}
