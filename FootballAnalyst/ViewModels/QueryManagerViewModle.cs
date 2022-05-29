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
    public class QueryManagerViewModel : ViewModelBase
    {
        private ObservableCollection<Table> m_tables;
        private ObservableCollection<Table> m_allTables;
        private ObservableCollection<Table> m_requests;
        private ObservableCollection<string> m_columnList;
        private ObservableCollection<Filter> m_filters;
        private ObservableCollection<Filter> m_groupFilters;
        private MainWindowViewModel m_mainWindow;
        private bool isBDTableSelected;
        internal Dictionary<string, string> Keys = new Dictionary<string, string>()
        {
            { "id_tournament", "id_tournament"},
            { "id_season", "id_season"},
            { "id_home_team", "id_home_team"},
            { "id_away_team", "id_away_team"},
            { "id_country", "id_country"},
            { "id_champion", "id_champion"},
            { "id_top_player", "id_top_player"}
        };

        public QueryManagerViewModel(ViewerViewModel _DBViewer, MainWindowViewModel _mainWindow)
        {
            DBViewer = _DBViewer;
            m_mainWindow = _mainWindow;
            m_tables = DBViewer.Tables;
            m_allTables = DBViewer.AllTables;
            m_requests = new ObservableCollection<Table>();
            m_filters = new ObservableCollection<Filter>();
            m_groupFilters = new ObservableCollection<Filter>();
            m_columnList = new ObservableCollection<string>();

            SelectedTables = new ObservableCollection<Table>();
            SelectedColumns = new ObservableCollection<string>();

            ResultTable = new List<Dictionary<string, object?>>();
            JoinedTable = new List<Dictionary<string, object?>>();
            SelectedColumnsTable = new List<Dictionary<string, object?>>();

            FilterChain = new FilterHandler(this, "Filters");
            GroupChain = new GroupHandler(this);
            GroupFilterChain = new FilterHandler(this, "GroupFilters");

            FilterChain.NextHope = GroupChain;
            GroupChain.NextHope = GroupFilterChain;

            IsRequestSuccess = false;
            isBDTableSelected = true;
        }

        public void UpdateColumnList()
        {
            ColumnList = new ObservableCollection<string>();
            if (JoinedTable.Count != 0)
            {
                foreach (var column in JoinedTable[0])
                {
                    ColumnList.Add(column.Key);
                }
            }
            Filters.Clear();
            GroupFilters.Clear();
        }

        public void AddRequest(string tableName)
        {
            FilterChain.Try();

            // Если запрос успешный добавляем результирующую таблицу в общий список и список запросов
            if (IsRequestSuccess)
            {
                ObservableCollection<string> properties = new ObservableCollection<string>();

                foreach (var item in ResultTable[0])
                {
                    properties.Add(item.Key);
                }

                Requests.Add(new Table(tableName, true, new QueryViewModel(ResultTable.ToList()), properties));
                AllTables.Add(Requests.Last());
            }

            IsBDTableSelected = true;

            // Очищаем всё
            ClearAll();

            // Открываем окно просмотра таблиц
            m_mainWindow.OpenViewerView();
        }

        public void DeleteRequests()
        {
            Requests = new ObservableCollection<Table>(Requests.Where(table => AllTables.Any(tables => tables.Name == table.Name)));
            GC.Collect();
        }

        public void ClearAll()
        {
            ResultTable.Clear();
            JoinedTable.Clear();
            SelectedColumnsTable.Clear();
            SelectedTables.Clear();
            SelectedColumns.Clear();
            Filters.Clear();
            GroupFilters.Clear();
            ColumnList.Clear();
        }

        private bool TryJoin(string key1, List<Dictionary<string, object?>> table2, string key2)
        {
            try
            {
                JoinedTable = JoinedTable.Join(
                    table2,
                    firstItem => firstItem[key1],
                    secondItem => secondItem[key2],
                    (firstItem, secondItem) =>
                    {
                        Dictionary<string, object?> resultItem = new Dictionary<string, object?>();
                        foreach (var item in firstItem)
                        {
                            resultItem.TryAdd(item.Key, item.Value);
                        }
                        foreach (var item in secondItem)
                        {
                            if (item.Key != key2)
                                resultItem.TryAdd(item.Key, item.Value);
                        }
                        return resultItem;
                    }
                    ).ToList();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Join()
        {
            if (SelectedTables.Count > 0)
            {
                var check = SelectedTables.Where(tab => tab.Name == "Matches");
                if (check.Count() != 0)
                {
                    Table tmp = check.Last();
                    SelectedTables.Remove(check.Last());
                    SelectedTables.Add(tmp);
                }
                JoinedTable = new List<Dictionary<string, object?>>(SelectedTables[0].Rows);

                if (SelectedTables.Count > 1)
                {
                    List<Dictionary<string, object?>> joiningTable;

                    bool success = false;
                    for (int i = 1; i < SelectedTables.Count; i++)
                    {
                        joiningTable = SelectedTables[i].Rows;

                        // Перебираем каждую пару ключей
                        foreach (var keysPair in Keys)
                        {
                            // И пытаемся соединить передав ключи возможной связи key1 - key2
                            success = TryJoin(keysPair.Key, joiningTable, keysPair.Value);
                            if (success)
                                break;

                            // Если не получилось, пытаемся передать key2 - key1
                            else
                            {
                                success = TryJoin(keysPair.Value, joiningTable, keysPair.Key);
                                if (success)
                                    break;
                            }
                        }
                        // Если никак не получилось соединить, то очищаем результирующий массив и обновляем список доступных колонок
                        if (!success)
                        {
                            JoinedTable.Clear();
                            ResultTable = JoinedTable;
                            UpdateColumnList();
                            return;
                        }
                    }
                }
                UpdateColumnList();
                ResultTable = JoinedTable;
            }

            else
            {
                JoinedTable.Clear();
                ResultTable = JoinedTable;
                ColumnList.Clear();
                IsBDTableSelected = true;
            }
        }

        public void Select()
        {
            SelectedColumnsTable = JoinedTable.Select(item =>
            {
                return new Dictionary<string, object?>(item.Where(property => SelectedColumns.Any(column => column == property.Key)));
            }).ToList();
            ResultTable = SelectedColumnsTable;
        }

        public bool IsRequestSuccess { get; set; }
        public bool IsBDTableSelected
        {
            get => isBDTableSelected;
            set
            {
                this.RaiseAndSetIfChanged(ref isBDTableSelected, value);
            }
        }
        public string? GroupingColumn { get; set; } = null;
        public List<Dictionary<string, object?>> ResultTable { get; set; }
        public List<Dictionary<string, object?>> JoinedTable { get; set; }
        public List<Dictionary<string, object?>> SelectedColumnsTable { get; set; }
        public ObservableCollection<string> ColumnList
        {
            get => m_columnList;
            set
            {
                this.RaiseAndSetIfChanged(ref m_columnList, value);
            }
        }
        public ObservableCollection<string> SelectedColumns { get; set; }
        public ObservableCollection<Filter> Filters
        {
            get => m_filters;
            set
            {
                this.RaiseAndSetIfChanged(ref m_filters, value);
            }
        }
        public ObservableCollection<Filter> GroupFilters
        {
            get => m_groupFilters;
            set
            {
                this.RaiseAndSetIfChanged(ref m_groupFilters, value);
            }
        }
        public ObservableCollection<Table> Tables
        {
            get => m_tables;
            set
            {
                this.RaiseAndSetIfChanged(ref m_tables, value);
            }
        }
        public ObservableCollection<Table> SelectedTables { get; set; }
        public ObservableCollection<Table> AllTables
        {
            get => m_allTables;
            set
            {
                this.RaiseAndSetIfChanged(ref m_allTables, value);
            }
        }
        public ObservableCollection<Table> Requests
        {
            get => m_requests;
            set
            {
                this.RaiseAndSetIfChanged(ref m_requests, value);
            }
        }
        public FilterHandler FilterChain { get; set; }
        public GroupHandler GroupChain { get; set; }
        public FilterHandler GroupFilterChain { get; set; }
        public ViewerViewModel DBViewer { get; }      
    }
}
