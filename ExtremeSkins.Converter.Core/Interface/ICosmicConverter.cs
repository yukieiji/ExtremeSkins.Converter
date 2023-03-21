namespace ExtremeSkins.Converter.Core.Interface;

public interface ICosmicConverter
{
    public string Author { get; init; }
    public string Name { get; init; }

    public void Convert(string targetPath);
}
