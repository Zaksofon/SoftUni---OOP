
using System;
using System.Linq;
using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Players;
using PlayersAndMonsters.Models.Players.Contracts;

namespace PlayersAndMonsters.Models.BattleFields
{
    public class BattleField : IBattleField
    {
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {

            if (attackPlayer.IsDead || enemyPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }

            if (attackPlayer.GetType().Name == nameof(Beginner))
            {
                attackPlayer.Health += 40;
                foreach (var card in attackPlayer.CardRepository.Cards)
                {
                    card.DamagePoints += 30;
                }
            }
                 
            if (enemyPlayer.GetType().Name == nameof(Beginner))
            {
                enemyPlayer.Health += 40;
                foreach (var card in enemyPlayer.CardRepository.Cards)
                {
                    card.DamagePoints += 30;
                }
            }

            var attackerHpBonus = attackPlayer.CardRepository.Cards
                .Select(c => c.HealthPoints)
                .Sum();
            attackPlayer.Health += attackerHpBonus;

            var enemyHpBonus = enemyPlayer.CardRepository.Cards
                .Select(c => c.HealthPoints)
                .Sum();
            enemyPlayer.Health += enemyHpBonus;

            while (!attackPlayer.IsDead && !enemyPlayer.IsDead)
            {
                enemyPlayer.TakeDamage(attackPlayer.CardRepository.Cards.Select(c => c.DamagePoints).Sum());

                if (enemyPlayer.IsDead)
                {
                    break;
                }
                attackPlayer.TakeDamage(enemyPlayer.CardRepository.Cards.Select(c => c.DamagePoints).Sum());
            }
        }
    }
}
