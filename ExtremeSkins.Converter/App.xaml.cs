using Prism.Ioc;

using System.Windows;

using ExtremeSkins.Converter.Views;
using ExtremeSkins.Converter.Service;

namespace ExtremeSkins.Converter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<FolderSelectDialogService, FolderSelectDialogService>();
            containerRegistry.RegisterSingleton<OpenExplorerService, OpenExplorerService>();
        }
    }
}
