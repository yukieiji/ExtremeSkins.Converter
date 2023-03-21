using System.IO;
using ExtremeSkins.Converter.Core.Interface;

using static ExtremeSkins.Converter.Core.Analyzer.SuperNewRoles.Define;

namespace ExtremeSkins.Converter.Core.Analyzer.SuperNewRoles;

public sealed class SuperNewNamePlatesAnalyzer : IRepositoryAnalyzer
{
    public string TargetPath { get; }

    public SuperNewNamePlatesAnalyzer(string targetPath)
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
        string visorJsonPath = Path.Combine(TargetPath, VisorDataJson);
        string namePlateJsonPath = Path.Combine(TargetPath, NamePlateDataJson);

        return
            File.Exists(visorJsonPath) &&
            File.Exists(namePlateJsonPath) &&
            File.Exists(hatJsonPath);
    }
}
