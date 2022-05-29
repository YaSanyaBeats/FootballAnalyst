using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using FootballAnalyst.ViewModels;
using System;

namespace FootballAnalyst.Views
{
    public partial class ViewerView : UserControl
    {
        public ViewerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void DeleteTab(object control, RoutedEventArgs args)
        {
            Button? btn = control as Button;
            if (btn != null)
            {
                ViewerViewModel? context = this.DataContext as ViewerViewModel;
                if (context != null)
                {
                    context.AllTables.Remove(btn.DataContext as Table);
                    GC.Collect();
                }
            }
        }
        private void SelectedTabChanged(object control, SelectionChangedEventArgs args)
        {
            TabControl? tabControl = control as TabControl;
            if (tabControl != null)
            {
                ViewerViewModel? context = this.DataContext as ViewerViewModel;
                Table? table = tabControl.SelectedItem as Table;
                if (context != null && table != null)
                {
                    context.CurrentTableName = table.Name;
                    context.CurrentTableIsSubtable = table.IsSubTable;
                }
            }
        }
    }
}
