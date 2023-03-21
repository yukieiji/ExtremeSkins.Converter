using System.IO;
using ExtremeSkins.Converter.Core.Interface;

using static ExtremeSkins.Converter.Core.Analyzer.TheOtherRoles.Define;

namespace ExtremeSkins.Converter.Core.Analyzer.TheOtherRoles;

public sealed class TheOtherHatsAnalyzer : IRepositoryAnalyzer
{
    public string TargetPath { get; }

    public TheOtherHatsAnalyzer(string targetPath)
    {
        this.TargetPath = targetPath;
    }

    public AnalyzeResult Analyze()
    {
        throw new System.NotImplementedException();
    }

    public bool IsValid()
    {
        string hatJsonPath = Path.Combine(TargetPath, HatDataJson);
        return File.Exists(hatJsonPath);
    }
}
