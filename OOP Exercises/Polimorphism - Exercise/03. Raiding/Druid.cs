
namespace _03.Raiding
{
    public class Druid : Hero
    {
        private const int power = 80;
        public Druid(string name) : base(name, power)
        {
            
        }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {Name} healed for {power}";
        }
    }
}
