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
    public class CountryTableViewModel : ViewModelBase
    {
        private ObservableCollection<Country> table;
        public CountryTableViewModel(ObservableCollection<Country> _drivers)
        {
            Table = _drivers;
            RemovableItems = new List<object>();
        }

        public ObservableCollection<Country> Table
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

        public override ObservableCollection<Country> GetTable()
        {
            return Table;
        }
    }
}
