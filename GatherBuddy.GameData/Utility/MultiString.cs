using System;
using Dalamud.Game;
using Dalamud.Plugin.Services;
using Lumina.Text;

namespace GatherBuddy.Utility;

public readonly struct MultiString(string en, string de, string fr, string jp, string kr)
{
    public static string ParseSeStringLumina(SeString? luminaString)
        => luminaString == null ? string.Empty : Dalamud.Game.Text.SeStringHandling.SeString.Parse(luminaString.RawData).TextValue;

    public readonly string English  = en;
    public readonly string German   = de;
    public readonly string French   = fr;
    public readonly string Japanese = jp;
    public readonly string Korean   = kr;

    public string this[ClientLanguage lang]
        => Name(ClientLanguage.Korean);

    public override string ToString()
        => Name(ClientLanguage.Korean);

    public string ToWholeString()
        => $"{English}|{German}|{French}|{Japanese}|{Korean}";


    public static MultiString FromPlaceName(IDataManager gameData, uint id)
    {
        var en = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.PlaceName>(ClientLanguage.English)!.GetRow(id)?.Name);
        var de = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.PlaceName>(ClientLanguage.German)!.GetRow(id)?.Name);
        var fr = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.PlaceName>(ClientLanguage.French)!.GetRow(id)?.Name);
        var jp = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.PlaceName>(ClientLanguage.Japanese)!.GetRow(id)?.Name);
        var kr = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.PlaceName>(ClientLanguage.Korean)!.GetRow(id)?.Name);
        return new MultiString(en, de, fr, jp, kr);
    }

    public static MultiString FromItem(IDataManager gameData, uint id)
    {
        var en = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.Item>(ClientLanguage.English)!.GetRow(id)?.Name);
        var de = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.Item>(ClientLanguage.German)!.GetRow(id)?.Name);
        var fr = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.Item>(ClientLanguage.French)!.GetRow(id)?.Name);
        var jp = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.Item>(ClientLanguage.Japanese)!.GetRow(id)?.Name);
        var kr = ParseSeStringLumina(gameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.Item>(ClientLanguage.Korean)!.GetRow(id)?.Name);
        return new MultiString(en, de, fr, jp, kr);
    }

    private string Name(ClientLanguage lang)
        => lang switch
        {
            ClientLanguage.English  => English,
            ClientLanguage.German   => German,
            ClientLanguage.Japanese => Japanese,
            ClientLanguage.French   => French,
            ClientLanguage.Korean   => Korean,
            _                       => throw new ArgumentException(),
        };

    public static readonly MultiString Empty = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
}
