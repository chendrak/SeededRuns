using HarmonyLib;
using RogueGenesia.Actors.Map;
using RogueGenesia.Data;
using RogueGenesia.Save;
using SeededRuns.Extensions;
using SeededRuns.UI.Helper;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace SeededRuns.UI;

[HarmonyPatch]
internal static class SeedOnMapControllerHooks
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(StageSelectionManager), nameof(StageSelectionManager.Start))]
    public static void OnStageSelectionManagerStartCalled(StageSelectionManager __instance)
    {
        SeedOnMapController.ShowSeedText();
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(GameData), nameof(GameData.OnStartNewGame))]
    [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LoadCurrentGame))]
    public static void OnPostGameStart()
    {
        SeededRuns.Log.LogDebug($"OnPostGameStart()");
        SeedOnMapController.SetCurrentSeed(GameData.seed);
    }
}

public static class SeedOnMapController
{
    private const int NumExistingElements = 4;
    private const int SpacingBetweenElements = 12;
    private const int ExtraSpacing = SpacingBetweenElements * NumExistingElements;

    private static GameObject? SeedTextParent { get; set; }
    private static Text? SeedText { get; set; }
    private static int _seed = -1;

    private static bool _shouldShow = false;

    public static void ShowSeedText()
    {
        _shouldShow = true;
        UpdateSeedText();
    }
    
    public static void SetCurrentSeed(int seed)
    {
        _seed = seed;
        UpdateSeedText();
    }

    private static void UpdateSeedText()
    {
        if (SeedText)
        {
            SeedText.text = $"Seed: {_seed}";
            SeedText.SetActive(_shouldShow);
        }
    }

    public static void OnStageTextFound(Text stageText)
    {
        var parent = stageText.transform.parent.gameObject;
        
        SeedTextParent = Object.Instantiate(parent, parent.transform, false);
        
        SeedTextParent.name = "SeedTextParent";
        
        SeedText = SeedTextParent.GetComponentInChildren<Text>();
        var csf = SeedText.gameObject.GetOrAddComponent<ContentSizeFitter>();
        csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        var tf = SeedTextParent.transform.TryCast<RectTransform>();
        if (tf)
        {
            var position = tf.position;
            var size = tf.sizeDelta;
            var newPosition = new Vector2(position.x, position.y - (size.y * NumExistingElements) - ExtraSpacing);
            tf.position = newPosition;
        }

        UpdateSeedText();
    }
}