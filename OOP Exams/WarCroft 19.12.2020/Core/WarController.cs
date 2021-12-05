using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
    {
        private readonly IList<Character> characterParty;
        private readonly Stack<Item> itemPool;

		public WarController()
        {
            characterParty = new List<Character>();
            itemPool = new Stack<Item>();
        }

		public string JoinParty(string[] args)
        {
            Character newCharacter = null;

            switch (args[0])
            {
				case nameof(Warrior): 
                    newCharacter = new Warrior(args[1]); break;

				case nameof(Priest):
                    newCharacter = new Priest(args[1]); break;

				default: throw new ArgumentException(String.Format(ExceptionMessages.InvalidCharacterType, args[0]));
            }

			characterParty.Add(newCharacter);
			return String.Format(SuccessMessages.JoinParty, args[1]);
        }

		public string AddItemToPool(string[] args)
        {
            Item newItem = null;

            switch (args[0])
            {
				case nameof(FirePotion):
                    newItem = new FirePotion(); break;

				case nameof(HealthPotion):
                    newItem = new HealthPotion(); break;

				default: throw new ArgumentException(String.Format(ExceptionMessages.InvalidItem, args[0]));
            }

			itemPool.Push(newItem);
			return String.Format(SuccessMessages.AddItemToPool, args[0]);
        }

		public string PickUpItem(string[] args)
        {
            var character = characterParty.FirstOrDefault(n => n.Name == args[0]);

            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, args[0]));	
            }

            if (itemPool.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

            var item = itemPool.Pop();
            character.Bag.AddItem(item);

            return string.Format(SuccessMessages.PickUpItem, character.Name, item.GetType().Name);
        }

		public string UseItem(string[] args)
		{
            var character = characterParty.FirstOrDefault(n => n.Name == args[0]);

            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, args[0]));
            }

            var currentItem = character.Bag.GetItem(args[1]);

            character.UseItem(currentItem);
            return String.Format(SuccessMessages.UsedItem, character.Name, currentItem.GetType().Name);
        }

		public string GetStats()
        {
            var characterStat = characterParty
                .OrderByDescending(h => h.IsAlive)
                .ThenByDescending(h => h.Health);

            StringBuilder sb = new StringBuilder();

            foreach (var character in characterStat)
            {
                sb.AppendLine(string.Format(SuccessMessages.CharacterStats, character.Name,
                    character.Health, character.BaseHealth,
                    character.Armor, character.BaseArmor, character.IsAlive ? "Alive" : "Dead"));
            }

            return sb.ToString().TrimEnd();
        }

		public string Attack(string[] args)
        {
            string attacker = args[0];
            string receiver = args[1];
            var currentAttacker = characterParty.FirstOrDefault(n => n.Name == attacker);
            var currentReceiver = characterParty.FirstOrDefault(n => n.Name == receiver);

            if (currentAttacker == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attacker));
            }

            if (currentReceiver == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiver));
            }

            Warrior warrior = currentAttacker as Warrior;

            if (warrior == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attacker));
            }

            warrior.Attack(currentReceiver);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(String.Format(SuccessMessages.AttackCharacter, attacker, receiver, currentAttacker.AbilityPoints,
                receiver, currentReceiver.Health, currentReceiver.BaseHealth, currentReceiver.Armor, currentReceiver.BaseArmor));

            if (currentReceiver.IsAlive == false)
            {
                sb.AppendLine(String.Format(SuccessMessages.AttackKillsCharacter, receiver));
            }

            return sb.ToString().TrimEnd();
        }

		public string Heal(string[] args)
        {
            string healer = args[0];
            string healingReceiver = args[1];
            var currentHealer = characterParty.FirstOrDefault(n => n.Name == healer);
            var currentHealingReceiver = characterParty.FirstOrDefault(n => n.Name == healingReceiver);

            if (currentHealer == null) 
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healer));
            }

            if (currentHealingReceiver == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingReceiver));
            }

            Priest priest = currentHealer as Priest;

            if (priest == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.HealerCannotHeal, healer));     
            }

            priest.Heal(currentHealingReceiver);
           
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format(SuccessMessages.HealCharacter, healer, healingReceiver, priest.AbilityPoints,
                healingReceiver, currentHealingReceiver.Health));

            return sb.ToString().TrimEnd();
        }
    }
}
