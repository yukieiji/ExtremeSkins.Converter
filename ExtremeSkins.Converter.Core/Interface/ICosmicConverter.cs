namespace ExtremeSkins.Converter.Core.Interface;

public interface ICosmicConverter
{
    public string Author { get; set; }
    public string Name { get; set; }

    public void Convert(string targetPath);
}
