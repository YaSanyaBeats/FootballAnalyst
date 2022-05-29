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
using System.Reactive.Linq;

namespace FootballAnalyst.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase page;
        private ViewerViewModel dbViewer;
        private QueryManagerViewModel queryManager;

        public ViewModelBase Page
        {
            set => this.RaiseAndSetIfChanged(ref page, value);
            get => page;
        }

        public MainWindowViewModel()
        {
            dbViewer = new ViewerViewModel();
            queryManager = new QueryManagerViewModel(dbViewer, this);
            Page = dbViewer;
        }

        public void OpenQueryView()
        {
            Page = queryManager;

            // И удаляем из списка запросов нужные таблицы, если такие есть
            queryManager.DeleteRequests();
        }

        public void OpenViewerView()
        {
            Page = dbViewer;
        }
    }
}
