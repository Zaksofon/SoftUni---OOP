
using System;
using System.Collections.Generic;
using System.Linq;
using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Repositories.Contracts;

namespace PlayersAndMonsters.Repositories
{
    public class  PlayerRepository : IPlayerRepository
    {
        private readonly IDictionary<string, IPlayer> players;

        public PlayerRepository()
        {
            players = new Dictionary<string, IPlayer>();
        }

        public int Count => players.Count;

        public IReadOnlyCollection<IPlayer> Players => players.Values.ToList().AsReadOnly();

        public void Add(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            if (players.ContainsKey(player.Username))
            {
                throw new ArgumentException($"Player {player.Username} already exists!");
            }

            players.Add(player.Username, player);
        }

        public bool Remove(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentException("Player cannot be null");
            }

            return players.Remove(player.Username);
        }

        public IPlayer Find(string username)
        {
            IPlayer player = null;

            if (players.ContainsKey(username))
            {
                 player = players[username];
            }
            return player;
        }
    }
}
