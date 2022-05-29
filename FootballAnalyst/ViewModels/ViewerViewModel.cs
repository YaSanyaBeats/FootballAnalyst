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
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;

namespace FootballAnalyst.ViewModels
{
    public class ViewerViewModel : ViewModelBase
    {
        private ObservableCollection<Table> m_tables;
        private ObservableCollection<Table> m_allTables;
        private ObservableCollection<Country> m_countryes;
        private ObservableCollection<Match> m_matches;
        private ObservableCollection<Player> m_players;
        private ObservableCollection<Season> m_seasons;
        private ObservableCollection<Team> m_teams;
        private ObservableCollection<Tournament> m_tournaments;
        private bool m_currentTableIsSubtable;

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
                            string property = properties[i].Remove(properties[i].IndexOf("("));
                            if (entityName == "Player" && property == "Id")
                                result.Add("IdTopPlayer");
                            else if (entityName == "Country" && property == "Id")
                                result.Add("IdCountry");
                            else if (entityName == "Tournament" && property == "IdTournaments")
                                result.Add("IdTournaments");
                            else if (entityName == "Season" && property == "Id")
                                result.Add("IdSeason");
                            else if (entityName == "Matches" && property == "Id")
                                result.Add("IdHomeTeam");
                            else
                                result.Add(property);
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
            try
            {
                m_tables = new ObservableCollection<Table>();
                DataBase = new dataContext();

                string tableInfo = DataBase.Model.ToDebugString();
                tableInfo = tableInfo.Replace(" ", "");
                string[] splitTableInfo = tableInfo.Split("\r\n");
                List<string> properties = new List<string>(splitTableInfo.Where(str => str.IndexOf("Entity") != -1 ||
                                                            (str.IndexOf("(") != -1 && str.IndexOf("<") == -1) &&
                                                            str.IndexOf("Navigation") == -1 && str.IndexOf("(Car)") == -1));

                DataBase.Countries.Load<Country>();
                Countries = DataBase.Countries.Local.ToObservableCollection();
                m_tables.Add(new Table("Country", false, new CountryTableViewModel(Countries), FindProperties("Country", properties)));

                DataBase.Matches.Load<Match>();
                Matches = DataBase.Matches.Local.ToObservableCollection();
                m_tables.Add(new Table("Matches", false, new MatchTableViewModel(Matches), FindProperties("Match", properties)));

                DataBase.Players.Load<Player>();
                Players = DataBase.Players.Local.ToObservableCollection();
                m_tables.Add(new Table("Players", false, new PlayerTableViewModel(Players), FindProperties("Player", properties)));

                DataBase.Seasons.Load<Season>();
                Seasons = DataBase.Seasons.Local.ToObservableCollection();
                m_tables.Add(new Table("Seasons", false, new SeasonTableViewModel(Seasons), FindProperties("Season", properties)));

                DataBase.Teams.Load<Team>();
                Teams = DataBase.Teams.Local.ToObservableCollection();
                m_tables.Add(new Table("Teams", false, new TeamTableViewModel(Teams), FindProperties("Team", properties)));

                DataBase.Tournaments.Load<Tournament>();
                Tournaments = DataBase.Tournaments.Local.ToObservableCollection();
                m_tables.Add(new Table("Tournaments", false, new TournamentTableViewModel(Tournaments), FindProperties("Tournament", properties)));

                AllTables = new ObservableCollection<Table>(Tables.ToList());

                CurrentTableName = "Country";

                CurrentTableIsSubtable = false;
            }
            catch
            {

            }
        }

        public bool CurrentTableIsSubtable
        {
            get => !m_currentTableIsSubtable;
            set => this.RaiseAndSetIfChanged(ref m_currentTableIsSubtable, value);
        }
        public string CurrentTableName { get; set; }
        public dataContext DataBase { get; set; }
        public ObservableCollection<Table> Tables
        {
            get => m_tables;
            set
            {
                this.RaiseAndSetIfChanged(ref m_tables, value);
            }
        }
        public ObservableCollection<Table> AllTables
        {
            get => m_allTables;
            set
            {
                this.RaiseAndSetIfChanged(ref m_allTables, value);
            }
        }
        public ObservableCollection<Country> Countries
        {
            get => m_countryes;
            set
            {
                this.RaiseAndSetIfChanged(ref m_countryes, value);
            }
        }
        public ObservableCollection<Match> Matches
        {
            get => m_matches;
            set
            {
                this.RaiseAndSetIfChanged(ref m_matches, value);
            }
        }
        public ObservableCollection<Player> Players
        {
            get => m_players;
            set
            {
                this.RaiseAndSetIfChanged(ref m_players, value);
            }
        }
        public ObservableCollection<Season> Seasons
        {
            get => m_seasons;
            set
            {
                this.RaiseAndSetIfChanged(ref m_seasons, value);
            }
        }
        public ObservableCollection<Team> Teams
        {
            get => m_teams;
            set
            {
                this.RaiseAndSetIfChanged(ref m_teams, value);
            }
        }
        public ObservableCollection<Tournament> Tournaments
        {
            get => m_tournaments;
            set
            {
                this.RaiseAndSetIfChanged(ref m_tournaments, value);
            }
        }

        public void AddItem()
        {
            switch (CurrentTableName)
            {
                case "Country":
                    {
                        Countries.Add(new Country());
                        break;
                    }
                case "Match":
                    {
                        Matches.Add(new Match());
                        break;
                    }
                case "Player":
                    {
                        Players.Add(new Player());
                        break;
                    }
                case "Season":
                    {
                        Seasons.Add(new Season());
                        break;
                    }
                case "Teams":
                    {
                        Teams.Add(new Team());
                        break;
                    }
                case "Tournament":
                    {
                        Tournaments.Add(new Tournament());
                        break;
                    }
            }
        }
        public void DeleteItems()
        {
            // Выбираем нужную таблицы
            Table currentTable = Tables.Where(table => table.Name == CurrentTableName).ToList()[0];

            // Получаем удаляемые элементы
            List<object>? RemovableItems = currentTable.GetRemovableItems();

            // Помечаем, что идет удаление, чтобы событие DataGrid'a на изменение выбранной строчки не срабатывало
            currentTable.SetRemoveInProgress(true);

            // Если список удаляемых элементов не пуст
            if (RemovableItems != null && RemovableItems.Count != 0)
            {

                // Выбираем таблицу по имени и удаляем элементы
                switch (CurrentTableName)
                {
                    case "Country":
                        {
                            for (int i = 0; i < RemovableItems.Count; i++)
                            {
                                Countries.Remove(RemovableItems[i] as Country);
                            }
                            break;
                        }
                    case "Match":
                        {
                            for (int i = 0; i < RemovableItems.Count; i++)
                            {
                                Matches.Remove(RemovableItems[i] as Match);
                            }
                            break;
                        }
                    case "Player":
                        {
                            for (int i = 0; i < RemovableItems.Count; i++)
                            {
                                Players.Remove(RemovableItems[i] as Player);
                            }
                            break;
                        }
                    case "Season":
                        {
                            for (int i = 0; i < RemovableItems.Count; i++)
                            {
                                Seasons.Remove(RemovableItems[i] as Season);
                            }
                            break;
                        }
                    case "Team":
                        {
                            for (int i = 0; i < RemovableItems.Count; i++)
                            {
                                Teams.Remove(RemovableItems[i] as Team);
                            }
                            break;
                        }
                    case "Tournament":
                        {
                            for (int i = 0; i < RemovableItems.Count; i++)
                            {
                                Tournaments.Remove(RemovableItems[i] as Tournament);
                            }
                            break;
                        }
                }
            }
            currentTable.SetRemoveInProgress(false);
        }
        public void Save()
        {
            DataBase.SaveChanges();
        }
    }
}
