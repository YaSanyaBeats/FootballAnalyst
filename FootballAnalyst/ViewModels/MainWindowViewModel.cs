using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using Microsoft.Data.Sqlite;
using System.IO;
using System;
using System.Reactive.Linq;

namespace FootballAnalyst.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase page;
        private ViewerViewModel viewerPage;
        private QueryViewModel queryPage;

        public ViewModelBase Page
        {
            set => this.RaiseAndSetIfChanged(ref page, value);
            get => page;
        }

        public MainWindowViewModel()
        {
            viewerPage = new ViewerViewModel();
            queryPage = new QueryViewModel();
            Page = viewerPage;
        }

        public void OpenQueryView()
        {
            Page = queryPage;
        }

        public void OpenViewerView()
        {
            Page = viewerPage;
        }
    }
}
