namespace ExtremeSkins.Converter.Service.Interface;

public interface IOokiiDialogService<T, R>
    where T : IOokiiDialogSetting
    where R : IOokiiDialogResult
{
    public R Show(T setting);
}
