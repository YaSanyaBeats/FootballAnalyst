using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FootballAnalyst.Views
{
    public partial class MatchTableView : UserControl
    {
        public MatchTableView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void DeleteNullColumn(object control, DataGridAutoGeneratingColumnEventArgs args)
        {
            if (args.PropertyName == "IdAwayTeamNavigation" || args.PropertyName == "IdHomeTeamNavigation")
            {
                args.Cancel = true;
            }
        }
    }
}
