namespace ExtremeSkins.Converter.Core.Analyzer.NebulaOnTheShip;

public static class Define
{
    /* Define JsonFile  */
    public const string DataJson = "Contents.json";
    public const string IgnoreImgName = "Empty.png";
    public const string ImgNameKey = "Address";


    /* Define HatData JsonFile Key */
    public const string HatDataFolder = "hats";

    public const string HatDataBodyKey = "hats";

    public const string HatNameKey = "Name";
    public const string HatAuthorKey = "Author";
    public const string HatFrontImgKey = "Main";
    public const string HatFrontFlipImgKey = "Flip";
    public const string HatBackImgKey = "Back";
    public const string HatBackFlipImgKey = "BackFlip";
    public const string HatClimbImgKey = "Climb";
    public const string HatAdaptiveKey = "Adaptive";
    public const string HatBounceKey = "Bounce";

    /*
     * "hats": [
     *   {
     *       "Author": 作者,
     *       "Name": スキン名,
     *       "Bounce": 文字列のブール,
     *       "Adaptive": 文字列のブール,
     *       "Behind": "false",
     *       "Main":{
     *          "Address" : 画像名.png,
     *       }
     *  },
     */


    // VisorDatas
    /* Define VisorData DataFolder */
    public const string VisorDataFolder = "visors";

    /* Define VisorData JsonFile Key */
    public const string VisorDataBodyKey = "visors";

    public const string VisorNameKey = "Name";
    public const string VisorAuthorKey = "Author";
    public const string VisorFrontImgKey = "Main";
    public const string VisorFrontFlipImgKey = "Flip";
    public const string VisorAdaptiveKey = "Adaptive";
    public const string VisorBehindHateKey = "BehindHat";

    /*
     * "visors": [
     *   {
     *       "Author": 作者,
     *       "Name": スキン名,
     *       "Bounce": 文字列のブール,
     *       "Adaptive": 文字列のブール,
     *       "Behind": "false",
     *       "Main":{
     *          "Address" : 画像名.png,
     *       }
     *  },
     */


    // NamePlateDatas
    /* Define NamePlateData DataFolder */
    public const string NamePlateDataFolder = "namePlates";

    /* Define NamePlateData JsonFile Key */
    public const string NamePlateDataBodyKey = "namePlates";

    public const string NamePlateNameKey = "Name";
    public const string NamePlateAuthorKey = "Author";
    public const string NamePlateImgKey = "Plate";
    /*
     * "hats": [
     *   {
     *       "Author": 作者,
     *       "Name": スキン名,
     *       "Plate":{
     *          "Address" : 画像名.png,
     *       }
     *  },
     */
}
