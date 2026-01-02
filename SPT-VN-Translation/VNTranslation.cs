using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using Range = SemanticVersioning.Range;

namespace VNTranslation;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.kobethuy.vntranslation";
    public override string Name { get; init; } = "VN Translation";
    public override string Author { get; init; } = "Kobe Thuy";
    public override List<string>? Contributors { get; init; }
    public override SemanticVersioning.Version Version { get; init; } = new("1.0.0");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~2.0.0") }
    };
    public override string? Url { get; init; } = "";
    public override bool? IsBundleMod { get; init; } = false;
    public override string? License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class VNTranslation(
    WTTServerCommonLib.WTTServerCommonLib wttCommon,
    ISptLogger<VNTranslation> logger) : IOnLoad
{
    public async Task OnLoad()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        await wttCommon.CustomLocaleService.CreateCustomLocales(assembly);
        logger.Success("[VN Translation] Custom locales loaded successfully.");
        await Task.CompletedTask;
    }
}