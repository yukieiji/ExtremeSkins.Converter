using Prism.Mvvm;
using Prism.Commands;

using System.Collections.ObjectModel;
using System.IO;

using ExtremeSkins.Converter.Service;
using ExtremeSkins.Converter.Service.Interface;

namespace ExtremeSkins.Converter.ViewModels;

public class MainWindowViewModel : BindableBase
{
    public string Title => "ExtremeSkins.Converter";

    public ObservableCollection<string> TargetRepository { get; private set; }

    public DelegateCommand<string> SetRepositoryCommand { get; private set; }
    public DelegateCommand ConvertCommand { get; private set; }
    public DelegateCommand OpenExportedFolderCommand { get; private set; }

    public string ExportLog
    {
        get { return exportLog; }
        set { SetProperty(ref exportLog, value); }
    }
    private string exportLog = string.Empty;

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

        this.ExportLog = string.Empty;
    }
    

    private void Convert()
    {
        this.isConverting = true;
        UpdateButton();

        string curDirPath = Directory.GetCurrentDirectory();
        string exportedDir = Path.Combine(curDirPath, outputDir);

        foreach (string path in this.TargetRepository)
        {
            var model = new Model.ConverterModel();
            foreach (string log in model.Convert(exportedDir, path))
            {
                this.ExportLog = $"{this.exportLog}\n{log}";
            }
        }
        
        UpdateButton();
        this.TargetRepository.Clear();
        this.isConverting = false;
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
        string addRepo;
        switch (type)
        {
            case "folder":
                var result = openFolderSelectDlgService.Show(
                    new FolderSelectDialogService.Setting()
                    {
                        Tilte = "SelectRepositoryFolder"
                    });

                if (result.State != IOokiiDialogResult.ShowState.Ok) { return; }

                addRepo = result.FolderPath;
                break;
            case "url":
                string url = Microsoft.VisualBasic.Interaction.InputBox(
                    "setUrl", "setUrl");
                if (string.IsNullOrEmpty(url)) { return; }
                addRepo = url;
                break;
            default:
                return;
        }

        this.TargetRepository.Add(addRepo);
        this.ConvertCommand.RaiseCanExecuteChanged();
    }
    private void UpdateButton()
    {
        this.ConvertCommand.RaiseCanExecuteChanged();
        this.SetRepositoryCommand.RaiseCanExecuteChanged();
    }
}
