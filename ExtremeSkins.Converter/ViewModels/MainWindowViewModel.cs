using Prism.Mvvm;
using Prism.Commands;
using System.Collections.ObjectModel;
using Microsoft.Win32;

using ExtremeSkins.Converter.Service;
using ExtremeSkins.Converter.Service.Interface;
using Prism.Services.Dialogs;

namespace ExtremeSkins.Converter.ViewModels;

public class MainWindowViewModel : BindableBase
{
    public string Title => "ExtremeSkins.Converter";

    public ObservableCollection<string> TargetRepository { get; private set; }
    public DelegateCommand<string> SetRepositoryCommand { get; private set; }

    private readonly IOokiiDialogService<
        FolderSelectDialogService.Setting,
        FolderSelectDialogService.Result> openFolderSelectDlgService;

    public MainWindowViewModel(
        IOokiiDialogService<
            FolderSelectDialogService.Setting,
            FolderSelectDialogService.Result> openFolderSelectService)
    {
        this.openFolderSelectDlgService = openFolderSelectService;
        this.SetRepositoryCommand = new DelegateCommand<string>(SetRepository);
        this.TargetRepository = new ObservableCollection<string>();
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
