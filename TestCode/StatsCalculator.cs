using System.Collections.Generic;
using System.Linq;
using TestCode.Models;

namespace TestCode
{
    public class StatsCalculator
    {
        public IEnumerable<Team> TeamReferenceData { get; set; }
        public IStatsWeighting StatsWeighting { get; set; }

        public StatsCalculator(IEnumerable<Team> teamReferenceData, IStatsWeighting statsWeighting)
        {
            TeamReferenceData = teamReferenceData;
            StatsWeighting = statsWeighting;
        }

        // TODO: Return the player for the specified player number, or null if not located.
        // The playerNumber parameter must be > 0. If it is not then return a null result.
        // Note
        //   Team.Players has the players for the team.
        //   Player.PlayerNumber is the field to be compared against
        public Player PlayerByPlayerNumber(int playerNumber)
        {
            var selectedPlayer = TeamReferenceData
                                    .Select(tfd => tfd.Players.Where(p => p.PlayerNumber == playerNumber).FirstOrDefault())
                                    .Where(tfd => tfd != null).FirstOrDefault();
            return selectedPlayer;
        }

        // TODO: For each team return their win % as well as their players win %, sorted by the team 'win %' highest to lowest.
        // If a teamId is specified then return data for only that team i.e. result list will only contain a single entry
        // otherwise if the teamId=0 return item data for each team in TeamReferenceData supplied in the constructor.
        // If a team is specified and cannot be located then return a empty list (list must be empty and not null).
        // NB! If any player on the team has played 100 or more matches then IStatsWeighting must be invoked with the required parameters
        //    ONLY make this call if one or more of the player matches is >= 100.
        //    The result must be stored in the PlayerWeighting field inside the TeamValue result class.
        //    If all the players within the team has played less than 100 matches each then PlayerWeighting must be set to 0.
        // Note
        //   Team Win % is Team.Victories over Team.Matches
        //   Player Win % is Player.Wins over Player.Matches i.e. the sum of all players win / matches on the team.
        public IEnumerable<TeamValue> TeamWinPercentage(int teamId = 0)
        {
            var teamValueList = TeamReferenceData
                                    .Select(tfd =>
                                    new
                                    {
                                        teamModel = tfd,
                                        teamWinsPercantage = percentageCalculator(tfd.Victories, tfd.Matches)
                                    })
                                    .ToList()
                                    .Select(twp =>
                                    new
                                    {
                                        team = twp,
                                        playerData = twp.teamModel.Players
                                        .Select(p => new
                                        {
                                            player = p,
                                            playerWinPercantage = percentageCalculator(p.Wins, p.Matches),
                                            PlayerWeighting = (p.Matches >= 100) ? StatsWeighting.Apply(percentageCalculator(p.Wins, p.Matches), p.Matches) : 0
                                        })
                                    }).ToList()
                                    .Select(twapwp => new TeamValue
                                    {
                                        Id = twapwp.team.teamModel.Id,
                                        Name = twapwp.team.teamModel.Name,
                                        TeamWinsPercentage = twapwp.team.teamWinsPercantage,
                                        PlayerWinPercentage = twapwp.playerData.Select(pd => pd.playerWinPercantage).Sum(),
                                        PlayerWeighting = (twapwp.playerData.Select(pd => pd.PlayerWeighting)).FirstOrDefault()
                                    })
                                    .OrderByDescending(tv => tv.TeamWinsPercentage)
                                    .ToList();

            if (teamId > 0)
            {
                if (teamValueList.Exists(tv => tv.Id == teamId))
                {
                    return teamValueList.Where(tv => tv.Id == teamId).ToList();
                }
                else
                {
                    return new List<TeamValue>();
                }
            }
            else
            {
                return teamValueList;
            }
        }
        private double percentageCalculator(float valueToDivide, float valueToDivideBy)
        {
            return valueToDivide / valueToDivideBy * 100;
        }
    }
}