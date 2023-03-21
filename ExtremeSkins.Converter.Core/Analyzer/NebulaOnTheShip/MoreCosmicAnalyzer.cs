using System.IO;
using ExtremeSkins.Converter.Core.Interface;

using static ExtremeSkins.Converter.Core.Analyzer.NebulaOnTheShip.Define;

namespace ExtremeSkins.Converter.Core.Analyzer.NebulaOnTheShip;

public sealed class MoreCosmicAnalyzer : IRepositoryAnalyzer
{
    public string TargetPath { get; init; }

    public AnalyzeResult Analyze()
    {
        throw new System.NotImplementedException();
    }

    public bool IsValid()
    {
        string jsonPath = Path.Combine(TargetPath, DataJson);
        return File.Exists(jsonPath);
    }
}
