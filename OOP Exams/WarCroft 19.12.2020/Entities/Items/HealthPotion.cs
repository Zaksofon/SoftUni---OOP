
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Entities
{
    public class HealthPotion : Item
    {
        private const int healthPotionWeight = 5;

        public HealthPotion() 
            : base(healthPotionWeight)
        {
        }

        public override void AffectCharacter(Character character)
        {
            base.AffectCharacter(character);

                character.Health += 20;
        }

    }
}
