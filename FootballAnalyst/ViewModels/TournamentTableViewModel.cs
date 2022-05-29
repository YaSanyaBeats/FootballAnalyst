using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using FootballAnalyst.Models;
using Microsoft.Data.Sqlite;
using System.IO;
using System;

namespace FootballAnalyst.ViewModels
{
    public class TournamentTableViewModel : ViewModelBase
    {
        private ObservableCollection<Tournament> table;
        public TournamentTableViewModel(ObservableCollection<Tournament> _drivers)
        {
            Table = _drivers;
            RemovableItems = new List<object>();
        }

        public ObservableCollection<Tournament> Table
        {
            get
            {
                return table;
            }
            set
            {
                table = value;
            }
        }

        public override ObservableCollection<Tournament> GetTable()
        {
            return Table;
        }
    }
}



