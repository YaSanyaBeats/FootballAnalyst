using System;
using System.Collections.Generic;

namespace FootballAnalyst.Models
{
    public partial class Player
    {
        public Player()
        {
            Tournaments = new HashSet<Tournament>();
        }

        public long Id { get; set; }
        public long? IdCountry { get; set; }
        public string FullName { get; set; } = null!;
        public long? IdTeam { get; set; }

        public virtual Country? IdCountryNavigation { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
