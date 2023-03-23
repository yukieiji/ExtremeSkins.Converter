using System.Collections.Generic;
using System.IO;

using ExtremeSkins.Converter.Core.Interface;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ExtremeSkins.Converter.Core.Extension;
using static ExtremeSkins.Converter.Core.Analyzer.NebulaOnTheShip.Define;

namespace ExtremeSkins.Converter.Core.Analyzer.NebulaOnTheShip;

public sealed class MoreCosmicAnalyzer : IRepositoryAnalyzer
{
    public string Name => "MoreCosmic";

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
                            GetImagePathFromJArryField(hat, HatFrontImgKey, HatDataFolder),
                        FrontFlipImagePath =
                            GetImagePathFromJArryField(hat, HatFrontFlipImgKey, HatDataFolder),
                        BackImagePath =
                            GetImagePathFromJArryField(hat, HatBackImgKey, HatDataFolder),
                        BackFlipImagePath =
                            GetImagePathFromJArryField(hat, HatBackFlipImgKey, HatDataFolder),
                        ClimbImagePath = 
                            GetImagePathFromJArryField(hat, HatClimbImgKey, HatDataFolder),
                        IsBound = hat.TryGetValue(HatBounceKey, out bool isHatBound) && isHatBound,
                        IsShader = hat.TryGetValue(HatAdaptiveKey, out bool isHatShader) && isHatShader,
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
                            GetImagePathFromJArryField(visor, VisorFrontImgKey, VisorDataFolder),
                        IdleFlipImagePath =
                            GetImagePathFromJArryField(visor, VisorFrontFlipImgKey, VisorDataFolder),
                        IsBehindHat = visor.TryGetValue(
                            VisorBehindHateKey, out bool isVisorBound) && isVisorBound,
                        IsShader = visor.TryGetValue(
                            VisorAdaptiveKey, out bool isVisorShader) && isVisorShader,
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
                        Author = namePlate.Value<string>(NamePlateAuthorKey),
                        Name = namePlate.Value<string>(NamePlateNameKey),
                        ImagePath =
                            GetImagePathFromJArryField(namePlate, NamePlateImgKey, NamePlateDataFolder),
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

    private string GetImagePathFromJArryField(JToken target, string arrayKey, string folder)
    {
        JArray arr = target.Value<JArray>(arrayKey);
        
        if (arr is null) { return string.Empty; }

        return Path.Combine(this.TargetPath, folder, arr.Value<string>(ImgNameKey));
    }
}
