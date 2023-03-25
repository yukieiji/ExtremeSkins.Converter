using Ookii.Dialogs.Wpf;
using ExtremeSkins.Converter.Service.Interface;

namespace ExtremeSkins.Converter.Service;

public sealed class FolderSelectDialogService : IOokiiDialogService<
    FolderSelectDialogService.Setting, FolderSelectDialogService.Result>
{
    public sealed class Setting : IOokiiDialogSetting
    {
        public string Tilte { get; set; } = string.Empty;
        public bool Multiselect { get; set; } = false;
        public bool ShowNewFolderButton { get; set; } = false;

    }
    public sealed class Result : IOokiiDialogResult
    {
        public IOokiiDialogResult.ShowState State { get; set; }
        public string FolderPath { get; set; }
    }

    public Result Show(Setting setting)
    {
        VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog()
        {
            Description = setting.Tilte,
            UseDescriptionForTitle = true,
            Multiselect = setting.Multiselect,
            ShowNewFolderButton = setting.ShowNewFolderButton,
        };

        bool? result = dlg.ShowDialog();

        bool isSetted = result.HasValue && result.Value;

        return new Result()
        {
            State = isSetted ? IOokiiDialogResult.ShowState.Ok : IOokiiDialogResult.ShowState.Cancel,
            FolderPath = dlg.SelectedPath,
        };

    }
}