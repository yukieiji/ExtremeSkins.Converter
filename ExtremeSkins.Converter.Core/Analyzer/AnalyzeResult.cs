using System.Collections.Generic;

namespace ExtremeSkins.Converter.Core.Analyzer;

public sealed class AnalyzeResult
{
    public string Name { get; init; }
    public List<ExtremeHatConverter>       Hat       { get; init; }
    public List<ExtremeVisorConverter>     Visor     { get; init; }
    public List<ExtremeNamePlateConverter> NamePlate { get; init; }
}
