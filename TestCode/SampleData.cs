using System.Collections.Generic;
using System.Linq;
using TestCode.Models;

namespace TestCode
{
    public static class SampleData
    {
        private static IQueryable<Player> _players;
        public static IQueryable<Team> Teams;

        static SampleData()
        {
            AddEntities();
        }

        private static void AddEntities()
        {
            _players = new List<Player>
            {
                new Player
                {
                    Name = "Todor Aleksiev",
                    TeamId = 1,
                    PlayerNumber = 1,
                    Championships = 1,
                    Matches = 184,
                    Wins = 9,
                    Podiums = 29
                },
                new Player
                {
                    Name = "Dante Amaral",
                    TeamId = 1,
                    PlayerNumber = 2,
                    Championships = 2,
                    Matches = 64,
                    Wins = 13,
                    Podiums = 33
                },
                new Player
                {
                    TeamId = 2,
                    Name = "Michele Baranowicz",
                    PlayerNumber = 3,
                    Championships = 7,
                    Matches = 262,
                    Wins = 91,
                    Podiums = 154
                },
                new Player
                {
                    TeamId = 2,
                    Name = "Andrea Bari",
                    PlayerNumber = 4,
                    Championships = 0,
                    Matches = 82,
                    Wins = 0,
                    Podiums = 5
                },
                new Player
                {
                    Name = "Michał Bąkiewicz",
                    TeamId = 3,
                    PlayerNumber = 5,
                    Championships = 0,
                    Matches = 55,
                    Wins = 7,
                    Podiums = 15
                },
                new Player
                {
                    Name = "Bronisław Bebel",
                    TeamId = 3,
                    PlayerNumber = 6,
                    Championships = 0,
                    Matches = 152,
                    Wins = 6,
                    Podiums = 16
                },
                new Player
                {
                    Name = "Facundo Conte",
                    TeamId = 4,
                    PlayerNumber = 7,
                    Championships = 0,
                    Matches = 128,
                    Wins = 11,
                    Podiums = 31
                },
                new Player
                {
                    Name = "Hugo Conte",
                    TeamId = 4,
                    PlayerNumber = 8,
                    Championships = 2,
                    Matches = 152,
                    Wins = 23,
                    Podiums = 58
                }
            }.AsQueryable();

            var teamList = new List<Team>
            {
                new Team
                {
                    Id = 1,
                    Name = "Beach Lovers",
                    Country = "Brazil",
                    Matches = 1678,
                    Victories = 368
                },
                new Team
                {
                    Id = 2,
                    Name = "Polish Devils",
                    Country = "Poland",
                    Matches = 1352,
                    Victories = 268
                },
                new Team
                {
                    Id = 3,
                    Name = "We play for food",
                    Country = "Italy",
                    Matches = 740,
                    Victories = 212
                },
                new Team
                {
                    Id = 4,
                    Name = "Rustlers",
                    Country = "Argentina",
                    Matches = 805,
                    Victories = 212
                }
            };

            teamList.ForEach(t => t.AddPlayers(_players.Where(d => d.TeamId == t.Id).ToList()));

            Teams = teamList.AsQueryable();
        }
    }
}