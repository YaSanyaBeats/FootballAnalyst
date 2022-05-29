using System;
using System.Collections.Generic;

namespace FootballAnalyst.Models
{
    public partial class Season
    {
        public Season()
        {
            Tournaments = new HashSet<Tournament>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "Id": return Id;
                    case "Name": return Name;
                }
                return null;
            }
        }
        public string Key()
        {
            return "Id";
        }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
