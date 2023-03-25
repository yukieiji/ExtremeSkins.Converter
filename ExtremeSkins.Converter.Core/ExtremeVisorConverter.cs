using System.IO;

using ExtremeSkins.Core.ExtremeHats;
using ExtremeSkins.Converter.Core.Interface;

namespace ExtremeSkins.Converter.Core;

public sealed class ExtremeHatConverter : ICosmicConverter
{
    public string Author { get; set; } = string.Empty;
    public string Name   { get; set; } = string.Empty;

    public string FrontImagePath     { get; init; } = string.Empty;
    public string FrontFlipImagePath { get; init; } = string.Empty;
    public string BackImagePath      { get; init; } = string.Empty;
    public string BackFlipImagePath  { get; init; } = string.Empty;
    public string ClimbImagePath     { get; init; } = string.Empty;
    
    public bool IsBound  { get; init; }
    public bool IsShader { get; init; }

    public void Convert(string targetPath)
    {
        string outputPath = DataStructure.GetHatPath(targetPath, Name);
        Utility.ForceRecreateFolder(outputPath);

        TryCopyFile(Path.Combine(
            outputPath, DataStructure.FrontImageName), FrontImagePath);

        bool hasFrontFlip = TryCopyFile(
            Path.Combine(outputPath, DataStructure.FrontFlipImageName),
            FrontFlipImagePath);
        bool hasBack = TryCopyFile(
            Path.Combine(outputPath, DataStructure.BackImageName),
            BackImagePath);
        bool hasBackFlip = TryCopyFile(
            Path.Combine(outputPath, DataStructure.BackFlipImageName),
            BackFlipImagePath);
        bool hasClimb = TryCopyFile(
            Path.Combine(outputPath, DataStructure.ClimbImageName),
            ClimbImagePath);

        HatInfo info = new HatInfo(
            Name: Name,
            Author: Author,
            Bound: IsBound,
            Shader: IsShader,
            Climb: hasClimb,
            FrontFlip: hasFrontFlip,
            Back: hasBack,
            BackFlip: hasBackFlip);

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
