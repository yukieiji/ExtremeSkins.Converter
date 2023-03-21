namespace ExtremeSkins.Converter.Core.Interface;

public interface IRepositoryConverter
{
    public bool IsValid();

    public ConvertResult Convert();
}
