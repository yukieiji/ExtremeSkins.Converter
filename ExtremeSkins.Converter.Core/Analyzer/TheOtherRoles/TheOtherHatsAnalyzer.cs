using System.Collections.Generic;
using System.IO;
using ExtremeSkins.Converter.Core.Interface;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ExtremeSkins.Converter.Core.Extension;

using static ExtremeSkins.Converter.Core.Analyzer.TheOtherRoles.Define;

namespace ExtremeSkins.Converter.Core.Analyzer.TheOtherRoles;

public sealed class TheOtherHatsAnalyzer : IRepositoryAnalyzer
{
    public string Name => "TheOtherHat";

    public string TargetPath { get; }

    public TheOtherHatsAnalyzer(string targetPath)
    {
        this.TargetPath = targetPath;
    }

    public AnalyzeResult Analyze()
    {
        using StreamReader hatJsonReader = File.OpenText(Path.Combine(TargetPath, HatDataJson));
        JObject hatJson = JObject.Load(new JsonTextReader(hatJsonReader));

        List<ExtremeHatConverter> hatConverter = new List<ExtremeHatConverter>();
        if (hatJson.TryGetValue(HatDataBodyKey, out JToken hatToken) &&
            hatToken is JArray hatDataArray)
        {
            foreach (JToken hat in hatDataArray)
            {
                hatConverter.Add(
                    new ExtremeHatConverter()
                    {
                        Author = hat.Value<string>(HatAuthorKey),
                        Name = hat.Value<string>(HatNameKey),
                        FrontImagePath = GetImagePath(hat, HatFrontImgKey, HatDataFolder),
                        FrontFlipImagePath = GetImagePath(hat, HatFrontFlipImgKey, HatDataFolder),
                        BackImagePath = GetImagePath(hat, HatBackImgKey, HatDataFolder),
                        BackFlipImagePath = GetImagePath(hat, HatBackFlipImgKey, HatDataFolder),
                        ClimbImagePath = GetImagePath(hat, HatClimbImgKey, HatDataFolder),
                        IsBound = hat.TryGetValue(HatBounceKey, out bool isHatBounce) && isHatBounce,
                        IsShader = hat.TryGetValue(HatAdaptiveKey, out bool isHatShader) && isHatShader,
                    }
                );
            }
        }
        return new AnalyzeResult()
        {
            Name = "TheOtherHat",
            Hat = hatConverter,
            NamePlate = new(),
            Visor = new(),
        };
    }

    public bool IsValid()
    {
        string hatJsonPath = Path.Combine(TargetPath, HatDataJson);
        return File.Exists(hatJsonPath);
    }

    private string GetImagePath(JToken token, string key, string folder)
    {
        string value = token.GetStringValue(key);

        if (string.IsNullOrEmpty(value)) { return string.Empty; }

        return Path.Combine(this.TargetPath, folder, value);
    }
}
