using System.Collections.Generic;
using System.IO;

using ExtremeSkins.Converter.Core.Interface;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using static ExtremeSkins.Converter.Core.Analyzer.NebulaOnTheShip.Define;

namespace ExtremeSkins.Converter.Core.Analyzer.NebulaOnTheShip;

public sealed class MoreCosmicAnalyzer : IRepositoryAnalyzer
{
    public string TargetPath { get; }

    public MoreCosmicAnalyzer(string targetPath)
    {
        this.TargetPath = targetPath;
    }

    public AnalyzeResult Analyze()
    {
        if (!IsValid())
        {
            throw new FileNotFoundException();
        }
        
        using StreamReader reader = File.OpenText(Path.Combine(TargetPath, DataJson));
        JObject contentJson = JObject.Load(new JsonTextReader(reader));

        List<ExtremeHatConverter> hatConverter = new List<ExtremeHatConverter>();
        if (contentJson.TryGetValue(HatDataBodyKey, out JToken hatToken) &&
            hatToken is JArray hatDataArray)
        {
            foreach (JToken hat in hatDataArray)
            {
                hatConverter.Add(
                    new ExtremeHatConverter()
                    {
                        Author = hat.Value<string>(HatAuthorKey),
                        Name = hat.Value<string>(HatNameKey),
                        FrontImagePath =
                            GetImagePathFromJArryField(hat, HatFrontImgKey),
                        FrontFlipImagePath =
                            GetImagePathFromJArryField(hat, HatFrontFlipImgKey),
                        BackImagePath =
                            GetImagePathFromJArryField(hat, HatBackImgKey),
                        BackFlipImagePath =
                            GetImagePathFromJArryField(hat, HatBackFlipImgKey),
                        ClimbImagePath = 
                            GetImagePathFromJArryField(hat, HatClimbImgKey),
                        IsBound = hat.Value<bool>(HatBounceKey),
                        IsShader = hat.Value<bool>(HatAdaptiveKey)
                    }
                );
            }
        }

        List<ExtremeVisorConverter> visorConverter = new List<ExtremeVisorConverter>();
        if (contentJson.TryGetValue(VisorDataBodyKey, out JToken visorToken) &&
            visorToken is JArray visorDataArray)
        {
            foreach (JToken visor in visorDataArray)
            {
                visorConverter.Add(
                    new ExtremeVisorConverter()
                    {
                        Author = visor.Value<string>(VisorAuthorKey),
                        Name = visor.Value<string>(VisorNameKey),
                        IdleImagePath =
                            GetImagePathFromJArryField(visor, VisorFrontImgKey),
                        IdleFlipImagePath =
                            GetImagePathFromJArryField(visor, VisorFrontFlipImgKey),
                        IsBehindHat = visor.Value<bool>(VisorBehindHateKey),
                        IsShader = visor.Value<bool>(VisorAdaptiveKey)
                    }
                );
            }
        }

        List<ExtremeNamePlateConverter> namePlateConverter = new List<ExtremeNamePlateConverter>();
        if (contentJson.TryGetValue(NamePlateDataBodyKey, out JToken namePlateToken) &&
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
        return new AnalyzeResult()
        {
            Name = "MoreCosmic",
            Hat = hatConverter,
            Visor = visorConverter,
            NamePlate = namePlateConverter,
        };
    }

    public bool IsValid()
    {
        string jsonPath = Path.Combine(TargetPath, DataJson);
        return File.Exists(jsonPath);
    }

    private static string GetImagePathFromJArryField(JToken target, string arrayKey)
    {
        JArray arr = target.Value<JArray>(arrayKey);
        
        if (arr is null) { return string.Empty; }

        return arr.Value<string>(ImgNameKey);
    }
}
