using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                string[] parts = input
                    .Split(";", StringSplitOptions.RemoveEmptyEntries);

                string command = parts[0];
                string teamName = parts[1];
                string playerName = default;
                try
                {
                    switch (command)
                    {
                        case "Team":
                            teams.Add(new Team(teamName));
                            break;

                        case "Add":
                            playerName = parts[2];
                            if (teams.Any(t => t.Name == teamName) == false)
                            {
                                throw new ArgumentException($"Team {teamName} does not exist.");
                            }

                            Player player = new Player(playerName, int.Parse(parts[3]), int.Parse(parts[4]),
                                int.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[7]));
                            teams.First(t => t.Name == teamName).AddPlayer(player);
                            break;

                        case "Remove":
                            if (teams.Any(t => t.Name == teamName) == false)
                            {
                                throw new ArgumentException($"Player {playerName} is not in [Team name] team.");
                            }

                            playerName = parts[2];
                            teams.First(t => t.Name == teamName).RemovePlayer(playerName);
                            break;

                        case "Rating":
                            if (teams.Any(t => t.Name == teamName) == false)
                            {
                                throw new ArgumentException($"Team {teamName} does not exist.");
                            }

                            Team team = teams.First(t => t.Name == teamName);
                            Console.WriteLine($"{teamName} - {team.TeamRating}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                
                }
            }
        }
    }
}
