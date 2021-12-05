
namespace SpaceStation.Models.Astronauts
{
   public class Biologist : Astronaut
   {
       private const double initialUnitsOfOxygen = 70;

        public Biologist(string name) 
            : base(name, initialUnitsOfOxygen)
        {
        }

        public override void Breath()
        {
            Oxygen -= 5;
        }
   }
}
