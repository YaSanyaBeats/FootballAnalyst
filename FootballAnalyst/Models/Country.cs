using System;
using System.Collections.Generic;

namespace FootballAnalyst.Models
{
    public partial class Country
    {
        public Country()
        {
            Players = new HashSet<Player>();
            Teams = new HashSet<Team>();
            Tournaments = new HashSet<Tournament>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Flag { get; set; } = null!;

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
