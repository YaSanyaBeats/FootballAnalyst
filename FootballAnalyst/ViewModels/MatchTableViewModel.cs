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
    public class MatchTableViewModel : ViewModelBase
    {
        private ObservableCollection<Match> table;
        public MatchTableViewModel(ObservableCollection<Match> _drivers)
        {
            Table = _drivers;
            RemovableItems = new List<object>();
        }

        public ObservableCollection<Match> Table
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

        public override ObservableCollection<Match> GetTable()
        {
            return Table;
        }
    }
}

