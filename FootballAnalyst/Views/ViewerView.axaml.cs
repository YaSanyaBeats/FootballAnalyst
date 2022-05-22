using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using FootballAnalyst.ViewModels;

namespace FootballAnalyst.Views
{
    public partial class ViewerView : UserControl
    {
        public ViewerView()
        {
            InitializeComponent();
            this.FindControl<Button>("AddRowButton").Click += delegate
            {
                ViewerViewModel? context = this.DataContext as ViewerViewModel;
                if (context != null)
                {
                    context.AddRow();
                }
            };
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
                    context.Tables.Remove(btn.DataContext as Table);
                    context.Requests.Remove(btn.DataContext as Table);
                }
            }
        }
        private void AddRow()
        {
            int a = 1;
        }
    }
}
