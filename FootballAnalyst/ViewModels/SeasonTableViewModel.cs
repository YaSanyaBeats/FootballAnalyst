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
    public class SeasonTableViewModel : ViewModelBase
    {
        private ObservableCollection<Season> table;
        public SeasonTableViewModel(ObservableCollection<Season> _drivers)
        {
            Table = _drivers;
        }

        public ObservableCollection<Season> Table
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

        public override ObservableCollection<Season> GetTable()
        {
            return Table;
        }
    }
}

