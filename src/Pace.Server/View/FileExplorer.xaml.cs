using Pace.Server.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pace.Server.View;

public partial class FileExplorer : UserControl
{
    public FileExplorer()
    {
        InitializeComponent();
    }

    private void OnFileEntryClicked(object sender, MouseButtonEventArgs e)
    {
        var vm = (FileExplorerViewModel)DataContext;
        vm.NavigateSelectedCommand.Execute(string.Empty);
    }
}
