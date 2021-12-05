
namespace PlayersAndMonsters.Models.Cards
{
    public class TrapCard : Card
    {
        private const int initialDamagePoints = 120;
        private const int initialHealthPoints = 5;

        public TrapCard(string name) 
            : base(name, initialDamagePoints, initialHealthPoints)
        {
        }
    }
}
