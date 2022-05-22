using System;
using System.Collections.Generic;

namespace FootballAnalyst.Models
{
    public partial class Tournament
    {
        public long IdTournament { get; set; }
        public long IdSeason { get; set; }
        public long? IdCountry { get; set; }
        public string Name { get; set; } = null!;
        public long? IdChampion { get; set; }
        public long? IdTopPlayer { get; set; }

        public virtual Team? IdChampionNavigation { get; set; }
        public virtual Country? IdCountryNavigation { get; set; }
        public virtual Season IdSeasonNavigation { get; set; } = null!;
        public virtual Player? IdTopPlayerNavigation { get; set; }
    }
}
