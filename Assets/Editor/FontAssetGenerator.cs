using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class FontAssetGenerator : Editor
{
    [MenuItem("Tools/Generate Korean Font Asset")]
    public static void GenerateKoreanFontAsset()
    {
        var fontPath = "Assets/Fonts/NotoSansKR-Medium.ttf";
        var sourceFont = AssetDatabase.LoadAssetAtPath<Font>(fontPath);

        if (sourceFont == null)
        {
            Debug.LogError("Font not found at: " + fontPath);
            return;
        }

        var characters =
            "기사마법궁수공격력방어레벨경험치체마나슬롯아이템장비스탯인벤토리퀵설정상점강화골드매드HP MP EXP ATK DEF Lv 0123456789/.%():ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz -+";

        var samplingPointSize = 90;
        var atlasPadding = 5;
        var renderMode = GlyphRenderMode.SDFAA;
        var atlasWidth = 4096;
        var atlasHeight = 4096;

        var fontAsset = TMP_FontAsset.CreateFontAsset(
            sourceFont,
            samplingPointSize,
            atlasPadding,
            renderMode,
            atlasWidth,
            atlasHeight);

        if (fontAsset == null)
        {
            Debug.LogError("Failed to create font asset");
            return;
        }

        var unicodeArray = new uint[characters.Length];
        for (var i = 0; i < characters.Length; i++) unicodeArray[i] = characters[i];

        fontAsset.TryAddCharacters(unicodeArray);

        fontAsset.name = "NotoSansKR SDF";

        var outputPath = "Assets/Fonts/NotoSansKR SDF.asset";

        var existingAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(outputPath);
        if (existingAsset != null) AssetDatabase.DeleteAsset(outputPath);

        AssetDatabase.CreateAsset(fontAsset, outputPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Korean Font Asset created at: " + outputPath + " with " + fontAsset.characterTable.Count +
                  " characters");
    }
}