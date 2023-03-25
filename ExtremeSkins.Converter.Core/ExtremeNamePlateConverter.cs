using System.IO;

using ExtremeSkins.Core.ExtremeNamePlate;
using ExtremeSkins.Converter.Core.Interface;

namespace ExtremeSkins.Converter.Core;

public sealed class ExtremeNamePlateConverter : ICosmicConverter
{
    public string Author { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public string ImagePath { get; init; } = string.Empty;

    public void Convert(string targetPath)
    {
        string outputPath = DataStructure.GetNamePlatePath(targetPath, Author, Name);
        string outputDir = Path.GetDirectoryName(outputPath);

        Utility.ForceRecreateFolder(outputDir);

        if (!File.Exists(ImagePath)) { return; }
        
        File.Copy(ImagePath, outputPath);
    }
}
