
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private List<Player> players;

        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }

        public double TeamRating => GetTeamRating();
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                name = value;
            }
        }
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(string name)
        {
           var playerToRemove = players.FirstOrDefault(p => p.Name == name);
            if (playerToRemove == null)
            {
                throw new InvalidOperationException($"Player {name} is not in {this.Name} team.");
            }

            players.Remove(playerToRemove);
        }

        public double GetTeamRating()
        {
            if (players.Any())
            {
                double rating = players.Sum(p => p.PlayerStatistics) / players.Count;
                return rating;
            }

            return default;
        }
    }
}
