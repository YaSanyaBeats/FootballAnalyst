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

    public class ViewerViewModel : ViewModelBase
    {
        private ObservableCollection<Table> tables;
        private ObservableCollection<Country> countries;
        private ObservableCollection<Match> matches;
        private ObservableCollection<Player> players;
        private ObservableCollection<Season> seasons;
        private ObservableCollection<Team> teams;
        private ObservableCollection<Tournament> tournaments;
        private ObservableCollection<Table> requests;

        private ObservableCollection<string> FindProperties(string entityName, List<string> properties)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            for (int i = 0; i < properties.Count(); i++)
            {
                if (properties[i].IndexOf("EntityType:" + entityName) != -1)
                {
                    try
                    {
                        i++;
                        while (properties[i].IndexOf("(") != -1 && i < properties.Count())
                        {
                            result.Add(properties[i].Remove(properties[i].IndexOf("(")));
                            i++;
                        }
                        return result;
                    }
                    catch
                    {
                        return result;
                    }
                }
            }
            return result;
        }

        public ViewerViewModel()
        {
            tables = new ObservableCollection<Table>();
            requests = new ObservableCollection<Table>();
            var DataBase = new dataContext();

            string tableInfo = DataBase.Model.ToDebugString();
            tableInfo = tableInfo.Replace(" ", "");
            string[] splitTableInfo = tableInfo.Split("\r\n");
            List<string> properties = new List<string>(splitTableInfo.Where(str => str.IndexOf("Entity") != -1 ||
                                                        (str.IndexOf("(") != -1 && str.IndexOf("<") == -1) &&
                                                        str.IndexOf("Navigation") == -1 && str.IndexOf("(Car)") == -1));



            countries = new ObservableCollection<Country>(DataBase.Countries);
            tables.Add(new Table("Countries", false, new CountryTableViewModel(countries), FindProperties("Countries", properties)));

            matches = new ObservableCollection<Match>(DataBase.Matches);
            tables.Add(new Table("Matches", false, new MatchTableViewModel(matches), FindProperties("Matches", properties)));

            players = new ObservableCollection<Player>(DataBase.Players);
            tables.Add(new Table("Players", false, new PlayerTableViewModel(players), FindProperties("Players", properties)));

            tournaments = new ObservableCollection<Tournament>(DataBase.Tournaments);
            tables.Add(new Table("Tournaments", false, new TournamentTableViewModel(tournaments), FindProperties("Tournaments", properties)));

            seasons = new ObservableCollection<Season>(DataBase.Seasons);
            tables.Add(new Table("Seasons", false, new SeasonTableViewModel(seasons), FindProperties("Seasons", properties)));

            teams = new ObservableCollection<Team>(DataBase.Teams);
            tables.Add(new Table("Teams", false, new TeamTableViewModel(teams), FindProperties("Team", properties)));
        }
        public void AddRow()
        {
            Countries.Add(new Country());
        }
        public ObservableCollection<Table> Tables
        {
            get => tables;
            set
            {
                this.RaiseAndSetIfChanged(ref tables, value);
            }
        }
        public ObservableCollection<Country> Countries
        {
            get => countries;
            set
            {
                this.RaiseAndSetIfChanged(ref countries, value);
            }
        }
        public ObservableCollection<Match> Matches
        {
            get => matches;
            set
            {
                this.RaiseAndSetIfChanged(ref matches, value);
            }
        }
        public ObservableCollection<Player> Players
        {
            get => players;
            set
            {
                this.RaiseAndSetIfChanged(ref players, value);
            }
        }
        public ObservableCollection<Season> Seasons
        {
            get => seasons;
            set
            {
                this.RaiseAndSetIfChanged(ref seasons, value);
            }
        }
        public ObservableCollection<Team> Teams
        {
            get => teams;
            set
            {
                this.RaiseAndSetIfChanged(ref teams, value);
            }
        }
        public ObservableCollection<Tournament> Tournaments
        {
            get => tournaments;
            set
            {
                this.RaiseAndSetIfChanged(ref tournaments, value);
            }
        }
        public ObservableCollection<Table> Requests
        {
            get => requests;
            set
            {
                this.RaiseAndSetIfChanged(ref requests, value);
            }
        }
    }

}
