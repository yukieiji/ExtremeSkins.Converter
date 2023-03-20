namespace ExtremeSkins.Converter.Core.Compat.SuperNewRoles;

public static class Define
{
    // HatDatas(TOH)
    /* Define HatData JsonFile and DataFolder  */
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
    public const string HatAdaptiveEKey = "adaptive";
    public const string HatBounceKey = "bounce";
    /*
     * "hats": [
     *   {
     *       "author": 作者,
     *       "bounce": ブール値,
     *       "resource": 画像名.png,
     *       "name": スキン名,
     *  },
     */


    // VisorDatas
    /* Define VisorData JsonFile and DataFolder */
    public const string VisorDataJson = "CustomVisors.json";
    public const string VisorDataFolder = "Visors";

    /* Define VisorData JsonFile Key */
    public const string VisorDataBodyKey = "Visors";

    public const string VisorNameKey = "name";
    public const string VisorAuthorKey = "author";
    public const string VisorImgKey = "resource";
    /*
     * "Visors": [
     *   {
     *       "author": 作者,
     *       "resource": 画像名.png,
     *       "name": スキン名,
     *  },
     */


    // NamePlateDatas
    /* Define NamePlateData JsonFile and DataFolder */
    public const string NamePlateDataJson = "CustomNamePlates.json";
    public const string NamePlateDataFolder = "NamePlates";

    /* Define NamePlateData JsonFile Key */
    public const string NamePlateDataBodyKey = "nameplates";

    public const string NamePlateNameKey = "name";
    public const string NamePlateAuthorKey = "author";
    public const string NamePlateImgKey = "resource";
    /*
     * "hats": [
     *   {
     *       "author": 作者,
     *       "resource": 画像名.png,
     *       "name": スキン名,
     *  },
     */
}
