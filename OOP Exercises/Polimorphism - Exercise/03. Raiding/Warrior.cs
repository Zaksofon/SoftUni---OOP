
namespace _03.Raiding
{
    public class Warrior : Hero
    {
        private const int power = 100;
        public Warrior(string name) : base(name, power)
        {
        }

        public override string CastAbility()
        {
           return $"{GetType().Name} - {Name} hit for {power} damage";
        }
    }
}
