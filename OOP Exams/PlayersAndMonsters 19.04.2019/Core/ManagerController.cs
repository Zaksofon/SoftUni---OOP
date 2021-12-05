using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PlayersAndMonsters.Common;
using PlayersAndMonsters.Models.BattleFields;
using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Cards;
using PlayersAndMonsters.Models.Cards.Contracts;
using PlayersAndMonsters.Models.Players;
using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories;
using PlayersAndMonsters.Repositories.Contracts;

namespace PlayersAndMonsters.Core
{
    using System;

    using Contracts;

    public class ManagerController : IManagerController
    {
        private readonly IPlayerRepository players;
        private readonly ICardRepository cards;
        private readonly IBattleField battleField;

        public ManagerController(IPlayerRepository players, ICardRepository cards, IBattleField battleField)
        {
            this.players = players;
            this.cards = cards;
            this.battleField = battleField;
        }

        public string AddPlayer(string type, string username)
        {
            IPlayer player = null;

            switch (type)
            {
                case nameof(Beginner):
                    player = new Beginner(new CardRepository(), username); break;

                case nameof(Advanced):
                    player = new Advanced(new CardRepository(), username); break;
            }

            players.Add(player);
            return String.Format(ConstantMessages.SuccessfullyAddedPlayer, type, username);
            //$"Successfully added player of type {type} with username: {username}";
        }

        public string AddCard(string type, string name)
        {
            ICard card = null;

            switch (type)
            {
                case nameof(MagicCard):
                    card = new MagicCard(name); break;

                case nameof(TrapCard):
                    card = new TrapCard(name); break;
            }

            cards.Add(card);
            return string.Format(ConstantMessages.SuccessfullyAddedCard, type, name);
            //$"Successfully added card of type {type} Card with name: {name}";
        }

        public string AddPlayerCard(string username, string cardName)
        {
            IPlayer player = players.Find(username);
            ICard card = cards.Find(cardName);

            player.CardRepository.Add(card);

            return string.Format(ConstantMessages.SuccessfullyAddedPlayerWithCards, cardName, username);
            //$"Successfully added card: {cardName} to user: {username}";
        }

        public string Fight(string attackUser, string enemyUser)
        {
            IPlayer attacker = players.Find(attackUser);
            IPlayer enemy = players.Find(enemyUser);

            battleField.Fight(attacker, enemy);

            return string.Format(ConstantMessages.SuccessfullyAddedPlayerWithCards, attacker.Health, enemy.Health);
            //$"Attack user health {attacker.Health} - Enemy user health {enemy.Health}";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var player in players.Players)
            {
                sb.AppendLine(string.Format(ConstantMessages.PlayerReportInfo, player.Username, player.Health, player.CardRepository.Cards.Count));
                //$"Username: {player.Username} - Health: {player.Health} – Cards {player.CardRepository.Cards.Count}"
                foreach (var card in cards.Cards)
                {
                    sb.AppendLine(string.Format(ConstantMessages.CardReportInfo, card.Name, card.DamagePoints));
                    //$"Card: {card.Name} - Damage: {card.DamagePoints}"
                }

                sb.AppendLine(string.Format(ConstantMessages.DefaultReportSeparator));
                //"###"
            }

            return sb.ToString().TrimEnd();
        }
    }
}
