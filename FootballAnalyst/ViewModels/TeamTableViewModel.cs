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
    public class TeamTableViewModel : ViewModelBase
    {
        private ObservableCollection<Team> table;
        public TeamTableViewModel(ObservableCollection<Team> _drivers)
        {
            Table = _drivers;
            RemovableItems = new List<object>();
        }

        public ObservableCollection<Team> Table
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

        public override ObservableCollection<Team> GetTable()
        {
            return Table;
        }
    }
}




