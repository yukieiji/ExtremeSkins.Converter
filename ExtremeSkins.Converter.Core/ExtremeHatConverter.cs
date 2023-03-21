using System.IO;

using ExtremeSkins.Core.ExtremeVisor;
using ExtremeSkins.Converter.Core.Interface;

namespace ExtremeSkins.Converter.Core;

public sealed class ExtremeVisorConverter : ICosmicConverter
{
    public string Author { get; init; } = string.Empty;
    public string Name   { get; init; } = string.Empty;

    public string IdleImagePath     { get; init; } = string.Empty;
    public string IdleFlipImagePath { get; init; } = string.Empty;
    
    public bool IsShader { get; init; }
    public bool IsBehindHat { get; init; }

    public void Convert(string targetPath)
    {
        string outputPath = DataStructure.GetVisorPath(targetPath, Name);
        Directory.CreateDirectory(outputPath);

        TryCopyFile(Path.Combine(
            outputPath, DataStructure.IdleImageName), IdleImagePath);

        bool hasFlip = TryCopyFile(
            Path.Combine(outputPath, DataStructure.FlipIdleImageName),
            IdleFlipImagePath);

        VisorInfo info = new VisorInfo(
            Name: Name,
            Author: Author,
            Shader: IsShader,
            LeftIdle: hasFlip,
            BehindHat: IsBehindHat);

        info.ExportToJson(outputPath);
    }

    private static bool TryCopyFile(string outputFile, string targetFile)
    {
        if (!string.IsNullOrEmpty(targetFile))
        {
            File.Copy(targetFile, outputFile);
            return true;
        }
        return false;
    }
}
