using System.Collections.Generic;
using System.IO;
using ExtremeSkins.Converter.Core.Interface;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ExtremeSkins.Converter.Core.Extension;
using static ExtremeSkins.Converter.Core.Analyzer.SuperNewRoles.Define;

namespace ExtremeSkins.Converter.Core.Analyzer.SuperNewRoles;

public sealed class SuperNewNamePlatesAnalyzer : IRepositoryAnalyzer
{
    public string Name => "SuperNewNamePlates";

    public string TargetPath { get; }

    public SuperNewNamePlatesAnalyzer(string targetPath)
    {
        this.TargetPath = targetPath;
    }

    public AnalyzeResult Analyze()
    {
        if (!IsValid())
        {
            throw new FileNotFoundException();
        }

        List<ExtremeHatConverter> hatConverter = new List<ExtremeHatConverter>();
        var hatAnalyzer = new TheOtherRoles.TheOtherHatsAnalyzer(TargetPath);
        if (hatAnalyzer.IsValid())
        {
            hatConverter = hatAnalyzer.Analyze().Hat;
        }

        string visorJsonPath = Path.Combine(TargetPath, VisorDataJson);
        List<ExtremeVisorConverter> visorConverter = new List<ExtremeVisorConverter>();
        if (File.Exists(visorJsonPath))
        {
            using StreamReader visorJsonReader = File.OpenText(Path.Combine(TargetPath, VisorDataJson));
            JObject visorJson = JObject.Load(new JsonTextReader(visorJsonReader));

            if (visorJson.TryGetValue(VisorDataBodyKey, out JToken visorToken) &&
                visorToken is JArray visorDataArray)
            {
                foreach (JToken visor in visorDataArray)
                {
                    visorConverter.Add(
                        new ExtremeVisorConverter()
                        {
                            Author = visor.Value<string>(VisorAuthorKey),
                            Name = visor.Value<string>(VisorNameKey),
                            IdleImagePath = visor.GetStringValue(VisorImgKey),
                            IdleFlipImagePath = string.Empty,
                            IsBehindHat = false,
                            IsShader = false
                        }
                    );
                }
            }
        }

        string namePlatePath = Path.Combine(TargetPath, NamePlateDataJson);
        List<ExtremeNamePlateConverter> namePlateConverter = new List<ExtremeNamePlateConverter>();
        if (File.Exists(namePlatePath))
        {
            using StreamReader namePlateJsonReader = File.OpenText(namePlatePath);
            JObject namePlateJson = JObject.Load(new JsonTextReader(namePlateJsonReader));

            if (namePlateJson.TryGetValue(NamePlateDataBodyKey, out JToken namePlateToken) &&
                namePlateToken is JArray namePlateDataArray)
            {
                foreach (JToken namePlate in namePlateDataArray)
                {
                    namePlateConverter.Add(
                        new ExtremeNamePlateConverter()
                        {
                            Author = namePlate.Value<string>(VisorAuthorKey),
                            Name = namePlate.Value<string>(VisorNameKey),
                            ImagePath =
                                GetImagePathFromJArryField(namePlate, NamePlateImgKey),
                        }
                    );
                }
            }
        }

        return new AnalyzeResult()
        {
            Name = "SuperNamePlates",
            Hat = hatConverter,
            Visor = visorConverter,
            NamePlate = namePlateConverter,
        };
    }

    public bool IsValid()
    {
        string visorJsonPath = Path.Combine(TargetPath, VisorDataJson);
        string namePlateJsonPath = Path.Combine(TargetPath, NamePlateDataJson);

        return
            File.Exists(visorJsonPath) ||
            File.Exists(namePlateJsonPath);
    }

    private static string GetImagePathFromJArryField(JToken target, string arrayKey)
    {
        string value = target.Value<string>(arrayKey);

        return string.IsNullOrEmpty(value) ? string.Empty : value;
    }
}
