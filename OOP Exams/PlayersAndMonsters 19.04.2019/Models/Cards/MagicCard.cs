
namespace PlayersAndMonsters.Models.Cards
{
    public class MagicCard : Card
    {
        private const int initialDamagePoints = 5;
        private const int initialHealthPoints = 80;

        public MagicCard(string name) 
            : base(name, initialDamagePoints, initialHealthPoints)
        {
        }
    }
}
