using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FootballAnalyst.Views
{
    public partial class CountryTableView : UserControl
    {
        public CountryTableView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DeleteNullColumn(object control, DataGridAutoGeneratingColumnEventArgs args)
        {
            if (args.PropertyName == "Players" || args.PropertyName == "Teams" || args.PropertyName == "Tournaments")
            {
                args.Cancel = true;
            }
        }
    }
}