using HarmonyLib;
using RogueGenesia.Data;
using SeededRuns.Helpers;
using UnityEngine.UI;
using UniverseLib;

namespace SeededRuns.UI;

[HarmonyPatch]
internal static class UiManagerStartGameHooks
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GameData), nameof(GameData.OnStartNewGame))]
    public static void OnGameDataStartNewGame(ref int _seed)
    {
        var lastUsedSeed = ConfigHelper.GetLastUsedSeed();
        SeededRuns.Log.LogDebug($"OnStartNewGame({_seed}) -> Setting new seed: {lastUsedSeed}");
        _seed = lastUsedSeed;
    }
}

public static class SeedPanelController
{
    private static SeedPanel? SeedPanel { get; set; }
    internal static Button? startRogModeButton;
    internal static Button? startSurvivorsModeButton;

    private static Button.ButtonClickedEvent startRogModeButtonClickedEvent;
    private static Button.ButtonClickedEvent startSurvivorsModeButtonClickedEvent;

    private static GameMode GameMode = GameMode.Rogs;

    public static void ShowSeedPanelForRogsMode()
    {
        ShowPanelForMode(GameMode.Rogs, startRogModeButtonClickedEvent);
    }

    public static void ShowSeedPanelForSurvivorsMode()
    {
        ShowPanelForMode(GameMode.Survivor, startSurvivorsModeButtonClickedEvent);
    }

    private static void ShowPanelForMode(GameMode gameMode, Button.ButtonClickedEvent clickedEvent)
    {
        if (SeedPanel == null) CreateSeedPanel();
        GameMode = gameMode;
        SeedPanel.SetButtonClickEvent(clickedEvent);
        SeedPanel.SetActive(true);
    }
    
    private static void CreateSeedPanel()
    {
        SeedPanel = new SeedPanel(UiManager.UiBase);
        SeedPanel.SetActive(false);
    }

    public static void ShowSeedPanel()
    {
        if (SeedPanel == null) CreateSeedPanel();
        SeedPanel.SetActive(true);
    }
    
    public static void HideSeedPanel()
    {
        SeedPanel.SetActive(false);
    }

    public static void SetRogButton(Button button)
    {
        startRogModeButton = button;
        startRogModeButtonClickedEvent = button.onClick;
        startRogModeButton.onClick = new Button.ButtonClickedEvent();
        startRogModeButton.onClick.AddListener(ShowSeedPanelForRogsMode);
    }

    public static void SetSurvivorsModeButton(Button button)
    {
        startSurvivorsModeButton = button;
        startSurvivorsModeButtonClickedEvent = button.onClick;
        startSurvivorsModeButton.onClick = new Button.ButtonClickedEvent();
        startSurvivorsModeButton.onClick.AddListener(ShowSeedPanelForSurvivorsMode);
    }
}