
using System;
using System.Collections.Generic;
using System.Linq;
using PlayersAndMonsters.Models.Cards.Contracts;
using PlayersAndMonsters.Repositories.Contracts;

namespace PlayersAndMonsters.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly IDictionary<string, ICard> cards;

        public CardRepository()
        {
            cards = new Dictionary<string, ICard>();
        }

        public int Count { get; }

        public IReadOnlyCollection<ICard> Cards => cards.Values.ToList().AsReadOnly();

        public void Add(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            if (cards.ContainsKey(card.Name))
            {
                throw new ArgumentException("Card {name} already exists!");
            }

            cards.Add(card.Name, card);
        }

        public bool Remove(ICard card)
        {
            if (card == null)
            {
                throw new ArgumentException("Card cannot be null!");
            }

            return cards.Remove(card.Name);
        }

        public ICard Find(string name)
        {
            ICard card = null;

            if (cards.ContainsKey(name))
            {
                card = cards[name];
            }

            return card;
        }
    }
}
