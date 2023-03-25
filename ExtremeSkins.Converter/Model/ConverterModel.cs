using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using AnyAscii;

using ExtremeSkins.Core;
using ExtremeSkins.Converter.Core;
using ExtremeSkins.Converter.Core.Analyzer;
using ExtremeSkins.Converter.Core.Interface;

using ExtremeHatDataStruct = ExtremeSkins.Core.ExtremeHats.DataStructure;
using ExtremeVisorDataStruct = ExtremeSkins.Core.ExtremeVisor.DataStructure;
using ExtremeNamePlateDataStruct = ExtremeSkins.Core.ExtremeNamePlate.DataStructure;
using SupportedLangs = ExtremeSkins.Core.CreatorMode.SupportedLangs;

namespace ExtremeSkins.Converter.Model;

internal class ConverterModel
{
    public string Locale { get; set; } = "ja-JP";
    public string AmongUsPath
    { 
        get => this.amongUsPath; 
        set
        {
            this.amongUsPath = value;
            this.path.Add(value);
        }
    }
    private string amongUsPath = string.Empty;
    
    private List<string> path = new List<string>();
    private Dictionary<string, string> transData = new Dictionary<string, string>();

    private static Dictionary<SupportedLangs, string> supportLnag = new Dictionary<SupportedLangs, string>()
    {
        {SupportedLangs.English   , ""},
        {SupportedLangs.Latam     , ""},
        {SupportedLangs.Brazilian , ""},
        {SupportedLangs.Portuguese, ""},
        {SupportedLangs.Korean    , ""},
        {SupportedLangs.Russian   , ""},
        {SupportedLangs.Dutch     , ""},
        {SupportedLangs.Filipino  , ""},
        {SupportedLangs.French    , ""},
        {SupportedLangs.German    , ""},
        {SupportedLangs.Italian   , ""},
        {SupportedLangs.Japanese  , "ja-JP"},
        {SupportedLangs.Spanish   , ""},
        {SupportedLangs.SChinese  , ""},
        {SupportedLangs.TChinese  , ""},
        {SupportedLangs.Irish     , ""},
    };

    public void AddOutPutPath(string outPath)
    {
        this.path.Add(outPath);
    }

    public IEnumerable<string> Convert(string targetRepo)
    {
        this.transData.Clear();

        IRepositoryAnalyzer analyzer;

        try
        {
            analyzer = RepositoryClassifier.Classify(targetRepo);
        }
        catch 
        {
            yield break;
        }

        string repoName = analyzer.Name;
        yield return $" ---- {repoName} Analyze Start!!  Path:{targetRepo} ----";

        AnalyzeResult result = analyzer.Analyze();

        foreach (string log in Execute(repoName, result))
        {
            yield return log;
        }

        if (!string.IsNullOrEmpty(this.amongUsPath))
        {
            CreatorMode.SetCreatorMode(this.amongUsPath, true);
        }

        yield return $" ---- END ----";
    }

    private IEnumerable<string> Execute(string repoName, AnalyzeResult result)
    {
        foreach (var (outputPath, index) in this.path.Select((item, index) => (item, index)))
        {
            foreach (string log in ConvertTargetPath(
                outputPath, repoName,
                result, index == 0))
            {
                yield return log;
            }
            yield return $" ---- Exporting Translation.... ----";
            ExportTranslationCsv(outputPath);
            yield return $" ---- Exporting Translation End ----";
        }
    }

    private IEnumerable<string> ConvertTargetPath(
        string targetPath, string analyzerName, AnalyzeResult result, bool isClean)
    {
        foreach (string log in ExecuteConvert(
            Path.Combine(targetPath, ExtremeHatDataStruct.FolderName),
            analyzerName, result.Hat, isClean))
        {
            yield return log;
        }
        foreach (string log in ExecuteConvert(
            Path.Combine(targetPath, ExtremeVisorDataStruct.FolderName),
            analyzerName, result.Visor, isClean))
        {
            yield return log;
        }
        foreach (string log in ExecuteConvert(
            Path.Combine(targetPath, ExtremeNamePlateDataStruct.FolderName),
            analyzerName, result.NamePlate, isClean))
        {
            yield return log;
        }
    }

    private void ExportTranslationCsv(string outPath)
    {
        List<string> writeStr = new List<string>();
        if (CreatorMode.IsExistTransFile(outPath))
        {
            using (StreamReader csv = CreatorMode.GetTranslationReader(outPath))
            {
                csv.ReadLine();
                while (!csv.EndOfStream)
                {
                    string line = csv.ReadLine();
                    if (!this.transData.Keys.Any(line.StartsWith))
                    {
                        writeStr.Add(csv.ReadLine());
                    }
                }
            }
        }

        foreach (var (transKey, trans) in this.transData)
        {
            StringBuilder builder = new StringBuilder(13);
            builder.Append(transKey).Append(CreatorMode.Comma);

            foreach (var local in supportLnag.Values)
            {
                builder.Append(
                    local == this.Locale ? trans : string.Empty).Append(CreatorMode.Comma);
            }
            writeStr.Add(builder.ToString());
        }

        using StreamWriter newCsv = CreatorMode.CreateTranslationWriter(outPath);
        foreach (string line in writeStr)
        {
            newCsv.WriteLine(line);
        }
    }

    private IEnumerable<string> ExecuteConvert<T>(
        string outputPath, string analyzerName, List<T> converterList, bool isClean) 
        where T : ICosmicConverter
    {
        if (converterList.Count <= 0)
        {
            yield break;
        }

        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        foreach (var converter in converterList)
        {
            yield return $"--- Converting.... Auther:{converter.Author} Name:{converter.Name}  ---";

            if (isClean)
            {
                string autherName = converter.Author;
                string conflictFixAutherName =
                    TryClean(autherName, out string asciiedAutherName) ?
                    $"{analyzerName}_{asciiedAutherName}" :
                    $"{analyzerName}_{autherName}";
                this.transData[conflictFixAutherName] = autherName;

                string skinName = converter.Name;
                string conflictFixSkinName =
                    TryClean(skinName, out string asciiedSkinName) ?
                    $"{analyzerName}_{asciiedSkinName}" :
                    $"{analyzerName}_{skinName}";
                this.transData[conflictFixSkinName] = skinName;

                converter.Name = conflictFixSkinName;
                converter.Author = conflictFixAutherName;
            }

            converter.Convert(outputPath);
        }
    }

    private static bool TryClean(string checkStr, out string replacedStr)
    {
        replacedStr = checkStr;
        bool isAscii = checkStr.IsAscii();
        if (!isAscii)
        {
            replacedStr = checkStr.Transliterate();
        }

        bool isReplace = false;
        char[] invalidFolderChar = Path.GetInvalidPathChars();
        foreach (char c in invalidFolderChar)
        {
            isReplace = TryReplecString(ref replacedStr, c) || isReplace;
        }

        char[] invalidFileChar = Path.GetInvalidFileNameChars();
        foreach (char c in invalidFileChar)
        {
            isReplace = TryReplecString(ref replacedStr, c) || isReplace;
        }
        isReplace = TryReplecString(ref replacedStr, '.') || isReplace;
        isReplace = TryReplecString(ref replacedStr, ' ') || isReplace;

        return !isAscii || isReplace;
    }

    private static bool TryReplecString(ref string replacedStr, char c)
    {
        bool isReplace = false;
        if (replacedStr.Contains(c))
        {
            isReplace = true;
            replacedStr = replacedStr.Replace(c.ToString(), "");
        }

        return isReplace;
    }
}
