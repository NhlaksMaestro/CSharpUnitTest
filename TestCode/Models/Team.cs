using System.Collections.Generic;

namespace TestCode.Models
{
    public class Team
    {
        private readonly List<Player> _players = new List<Player>();

        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }


        public int Matches { get; set; }

        public int Victories { get; set; }

        public virtual ICollection<Player> Players
        {
            get { return _players; }
        }

        internal void AddPlayers(List<Player> players)
        {
            _players.AddRange(players);
        }
    }
}