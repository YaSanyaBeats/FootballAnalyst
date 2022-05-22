using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FootballAnalyst.Views
{
    public partial class TeamTableView : UserControl
    {
        public TeamTableView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void DeleteNullColumn(object control, DataGridAutoGeneratingColumnEventArgs args)
        {
            if (args.PropertyName == "IdCountryNavigation" || args.PropertyName == "MatchIdAwayTeamNavigations" || args.PropertyName == "MatchIdHomeTeamNavigations" || args.PropertyName == "Tournaments")
            {
                args.Cancel = true;
            }
        }
    }
}
