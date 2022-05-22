using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FootballAnalyst.Views
{
    public partial class TournamentTableView : UserControl
    {
        public TournamentTableView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void DeleteNullColumn(object control, DataGridAutoGeneratingColumnEventArgs args)
        {
            if (args.PropertyName == "IdChampionNavigation" || args.PropertyName == "IdCountryNavigation" || args.PropertyName == "IdSeasonNavigation" || args.PropertyName == "IdTopPlayerNavigation")
            {
                args.Cancel = true;
            }
        }
    }
}
