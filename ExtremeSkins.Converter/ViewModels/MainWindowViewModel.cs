using Prism.Mvvm;
using Prism.Commands;

using System.Collections.ObjectModel;

using ExtremeSkins.Converter.Service;
using ExtremeSkins.Converter.Service.Interface;
using System.IO;

namespace ExtremeSkins.Converter.ViewModels;

public class MainWindowViewModel : BindableBase
{
    public string Title => "ExtremeSkins.Converter";

    public ObservableCollection<string> TargetRepository { get; private set; }

    public DelegateCommand<string> SetRepositoryCommand { get; private set; }
    public DelegateCommand ConvertCommand { get; private set; }
    public DelegateCommand OpenExportedFolderCommand { get; private set; }

    private readonly FolderSelectDialogService openFolderSelectDlgService;
    private readonly OpenExplorerService openExplorerService;

    private bool isConverting = false;
    private const string outputDir = "output";

    public MainWindowViewModel(
        FolderSelectDialogService openFolderSelectService,
        OpenExplorerService openExplorerService)
    {
        this.openFolderSelectDlgService = openFolderSelectService;
        this.openExplorerService = openExplorerService;
        this.SetRepositoryCommand = new DelegateCommand<string>(SetRepository, IsSetRepositoryCheck);
        this.ConvertCommand = new DelegateCommand(Convert, IsExecuteConvert);
        this.OpenExportedFolderCommand = new DelegateCommand(OpenExportedFolder);
        this.TargetRepository = new ObservableCollection<string>();
    }
    

    private void Convert()
    {
        this.isConverting = true;
        foreach (string path in this.TargetRepository)
        {

        }
    }

    private bool IsExecuteConvert() => this.TargetRepository.Count > 0;

    private bool IsSetRepositoryCheck(string _) => !this.isConverting;

    private void OpenExportedFolder()
    {
        string curDirPath = Directory.GetCurrentDirectory();
        string exportedDir = Path.Combine(curDirPath, outputDir);

        if (!Directory.Exists(exportedDir))
        {
            Directory.CreateDirectory(exportedDir);
        }

        this.openExplorerService.Open(new OpenExplorerService.Setting
        {
            TargetPath = exportedDir,
        });
    }

    private void SetRepository(string type)
    {

        switch (type)
        {
            case "folder":
                var result = openFolderSelectDlgService.Show(
                    new FolderSelectDialogService.Setting()
                    {
                        Tilte = "SelectRepositoryFolder"
                    });

                if (result.State != IOokiiDialogResult.ShowState.Ok) { return; }

                this.TargetRepository.Add(result.FolderPath);
                break;
            case "url":
                string url = Microsoft.VisualBasic.Interaction.InputBox(
                    "setUrl", "setUrl");
                if (string.IsNullOrEmpty(url)) { return; }

                this.TargetRepository.Add(url);
                break;
            default:
                break;
        }
    }
}
