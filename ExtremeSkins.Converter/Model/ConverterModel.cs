using System.Collections.Generic;
using System.IO;

using ExtremeSkins.Converter.Core;
using ExtremeSkins.Converter.Core.Analyzer;
using ExtremeSkins.Converter.Core.Interface;

using ExtremeHatDataStruct = ExtremeSkins.Core.ExtremeHats.DataStructure;
using ExtremeVisorDataStruct = ExtremeSkins.Core.ExtremeVisor.DataStructure;
using ExtremeNamePlateDataStruct = ExtremeSkins.Core.ExtremeNamePlate.DataStructure;

namespace ExtremeSkins.Converter.Model;

internal class ConverterModel
{
    public IEnumerable<string> Convert(
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

        foreach (string log in ExecuteConvert(
            Path.Combine(outPath, ExtremeHatDataStruct.FolderName), result.Hat))
        {
            yield return log;
        }

        foreach (string log in ExecuteConvert(
            Path.Combine(outPath, ExtremeVisorDataStruct.FolderName), result.Visor))
        {
            yield return log;
        }

        foreach (string log in ExecuteConvert(
            Path.Combine(outPath, ExtremeNamePlateDataStruct.FolderName), result.NamePlate))
        {
            yield return log;
        }

        yield return $" ---- END ----";
    }

    private IEnumerable<string> ExecuteConvert<T>(string outputPath, List<T> converterList) 
        where T : ICosmicConverter
    {
        if (converterList.Count <= 0)
        {
            yield break;
        }

        Utility.ForceRecreateFolder(outputPath);

        foreach (var converter in converterList)
        {
            yield return $"--- Converting.... Auther:{converter.Name} Name:{converter.Name}  ---";
            converter.Convert(outputPath);
        }
    }
}
