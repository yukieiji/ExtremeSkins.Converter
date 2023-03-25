using System.IO;

using ExtremeSkins.Core;
using ExtremeSkins.Core.ExtremeVisor;
using ExtremeSkins.Converter.Core.Interface;

namespace ExtremeSkins.Converter.Core;

public sealed class ExtremeVisorConverter : ICosmicConverter
{
  
    public string Author { get; set; } = string.Empty;
    public string Name   { get; set; } = string.Empty;

    public string IdleImagePath     { get; init; } = string.Empty;
    public string IdleFlipImagePath { get; init; } = string.Empty;
    
    public bool IsShader { get; init; }
    public bool IsBehindHat { get; init; }

    public void Convert(string targetPath)
    {
        string outputPath = DataStructure.GetVisorPath(targetPath, Name);
        Utility.ForceRecreateFolder(outputPath);

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

        InfoBase.ExportToJson(info, outputPath);
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
