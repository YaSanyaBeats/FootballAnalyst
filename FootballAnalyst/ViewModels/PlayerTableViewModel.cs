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
    public class PlayerTableViewModel : ViewModelBase
    {
        private ObservableCollection<Player> table;
        public PlayerTableViewModel(ObservableCollection<Player> _drivers)
        {
            Table = _drivers;
        }

        public ObservableCollection<Player> Table
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

        public override ObservableCollection<Player> GetTable()
        {
            return Table;
        }
    }
}


