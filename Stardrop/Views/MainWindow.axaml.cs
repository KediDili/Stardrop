using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Collections;
using Stardrop.Models;
using System.ComponentModel;
using Avalonia.Data;

namespace Stardrop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dataGridSortDescription = DataGridSortDescription.FromPath(nameof(Mod.Name), ListSortDirection.Ascending);

            // Set the path according to the environmental variable SMAPI_MODS_PATH
            // SMAPI_MODS_PATH is set via the profile dropdown on the UI
            var modView = new DataGridCollectionView(Mods.GetMods(Program.defaultModPath));
            modView.SortDescriptions.Add(dataGridSortDescription);
            var modGrid = this.FindControl<DataGrid>("modGrid"); modGrid.IsReadOnly = true;
            modGrid.LoadingRow += Dg1_LoadingRow;
            modGrid.Sorting += (s, a) =>
            {
                var binding = (a.Column as DataGridBoundColumn)?.Binding as Binding;

                if (binding?.Path is string property
                    && property == dataGridSortDescription.PropertyPath
                    && !modView.SortDescriptions.Contains(dataGridSortDescription))
                {
                    modView.SortDescriptions.Add(dataGridSortDescription);
                }
            };
            modGrid.Items = modView;


#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void Dg1_LoadingRow(object? sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
