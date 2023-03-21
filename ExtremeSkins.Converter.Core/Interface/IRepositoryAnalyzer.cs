namespace ExtremeSkins.Converter.Core.Interface;

public interface IRepositoryAnalyzer
{
    public bool IsValid();

    public AnalyzeResult Analyze();
}
