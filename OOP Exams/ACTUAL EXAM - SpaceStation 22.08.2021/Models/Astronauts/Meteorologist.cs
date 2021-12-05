
namespace SpaceStation.Models.Astronauts
{
    public class Meteorologist : Astronaut
    {
        private const double initialUnitsOfOxygen = 90;

        public Meteorologist(string name) 
            : base(name, initialUnitsOfOxygen)
        {
        }
    }
}
