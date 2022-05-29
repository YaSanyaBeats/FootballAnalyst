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

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "Id": return Id;
                    case "IdCountry": return IdCountry;
                    case "FullName": return FullName;
                    case "IdTeam": return IdTeam;
                }
                return null;
            }
        }
        public string Key()
        {
            return "Id";
        }

        public virtual Country? IdCountryNavigation { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
