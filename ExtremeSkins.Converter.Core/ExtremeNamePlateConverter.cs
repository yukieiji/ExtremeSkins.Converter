using System.IO;

using ExtremeSkins.Core.ExtremeNamePlate;
using ExtremeSkins.Converter.Core.Interface;

namespace ExtremeSkins.Converter.Core;

public sealed class ExtremeNamePlateConverter : ICosmicConverter
{
    public string Author { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;

    public string ImagePath { get; init; } = string.Empty;

    public void Convert(string targetPath)
    {
        string outputPath = DataStructure.GetNamePlatePath(targetPath, Name);
        string outputDir = Path.GetDirectoryName(outputPath);

        Utility.ForceRecreateFolder(outputDir);
        File.Copy(ImagePath, outputPath);
    }
}
