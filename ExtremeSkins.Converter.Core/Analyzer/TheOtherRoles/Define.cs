namespace ExtremeSkins.Converter.Core.Analyzer.TheOtherRoles;

public static class Define
{
    // HatDatas
    /* Define HatData JsonFile and DataFolder */
    public const string HatDataJson = "CustomHats.json";
    public const string HatDataFolder = "hats";

    /* Define HatData JsonFile Key */
    public const string HatDataBodyKey = "hats";

    public const string HatNameKey = "name";
    public const string HatAuthorKey = "author";
    public const string HatFrontImgKey = "resource";
    public const string HatFrontFlipImgKey = "flipresource";
    public const string HatBackImgKey = "backresource";
    public const string HatBackFlipImgKey = "backflipresource";
    public const string HatClimbImgKey = "climbresource";
    public const string HatAdaptiveKey = "adaptive";
    public const string HatBounceKey = "bounce";
    /*
     * "hats": [
     *   {
     *       "author": 作者,
     *       "bounce": ブール値,
     *       "resource": 画像名.png,
     *       "condition": ,
     *       "name": スキン名,
     *  },
     */
}
