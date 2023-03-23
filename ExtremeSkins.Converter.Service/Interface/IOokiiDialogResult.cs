namespace ExtremeSkins.Converter.Service.Interface;

public interface IOokiiDialogResult
{
    public enum ShowState
    {
        InvalidSetting,
        Cancel,
        Ok,
    }
    public ShowState State { get; set; }
}
