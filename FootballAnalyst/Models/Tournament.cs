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

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "IdTournament": return IdTournament;
                    case "IdSeason": return IdSeason;
                    case "IdCountry": return IdCountry;
                    case "Name": return Name;
                    case "IdChampion": return IdChampion;
                    case "IdTopPlayer": return IdTopPlayer;
                }
                return null;
            }
        }
        public string Key()
        {
            return "Id";
        }

        public virtual Team? IdChampionNavigation { get; set; }
        public virtual Country? IdCountryNavigation { get; set; }
        public virtual Season IdSeasonNavigation { get; set; } = null!;
        public virtual Player? IdTopPlayerNavigation { get; set; }
    }
}
