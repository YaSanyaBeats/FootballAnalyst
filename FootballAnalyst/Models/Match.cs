using System;
using System.Collections.Generic;

namespace FootballAnalyst.Models
{
    public partial class Match
    {
        public long Id { get; set; }
        public long? IdTournament { get; set; }
        public long? IdSeason { get; set; }
        public string Time { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Referee { get; set; } = null!;
        public string Venue { get; set; } = null!;
        public long IdHomeTeam { get; set; }
        public long? HomeTeamGoals { get; set; }
        public long IdAwayTeam { get; set; }
        public long? AwayTeamGoals { get; set; }

        public virtual Team IdAwayTeamNavigation { get; set; } = null!;
        public virtual Team IdHomeTeamNavigation { get; set; } = null!;
    }
}
