using System.Collections.Generic;
using System.IO;

using ExtremeSkins.Converter.Core;
using ExtremeSkins.Converter.Core.Analyzer;
using ExtremeSkins.Converter.Core.Interface;

namespace ExtremeSkins.Converter.Model;

internal class ConverterModel
{
    public IEnumerator<string> Convert(
        string outPath, string targetRepo)
    {
        IRepositoryAnalyzer analyzer;

        try
        {
            analyzer = RepositoryClassifier.Classify(targetRepo);
        }
        catch 
        {
            yield break;
        }

        yield return $" ---- {analyzer.Name} Analyze Start!!  Path{targetRepo} ----";

        AnalyzeResult result = analyzer.Analyze();

        yield return ExecuteConvert(outPath, result.Hat);
        yield return ExecuteConvert(outPath, result.Visor);
        yield return ExecuteConvert(outPath, result.NamePlate);
    }

    private IEnumerator<string> ExecuteConvert<T>(string outputPath, List<T> converterList) 
        where T : ICosmicConverter
    {
        if (converterList.Count <= 0)
        {
            yield break;
        }

        if (Directory.Exists(outputPath))
        {
            Directory.Delete(outputPath, true);
        }

        Directory.CreateDirectory(outputPath);

        foreach (var converter in converterList)
        {
            converter.Convert(outputPath);
        }
    }
}
