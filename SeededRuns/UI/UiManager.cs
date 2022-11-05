#nullable enable
using System.Collections;
using System.IO;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP.Utils;
using HarmonyLib;
using RogueGenesia.Data;
using RogueGenesia.UI;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib;
using UniverseLib.UI;

namespace SeededRuns.UI;

[HarmonyPatch]
internal static class UiManagerStartGameHooks
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GameData), nameof(GameData.OnStartNewGame))]
    public static void OnGameDataStartNewGame(ref int _seed)
    {
        SeededRuns.Log.LogInfo($"OnStartNewGame({_seed})");
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public static void OnMainMenuManagerStartCalled(MainMenuManager __instance)
    {
        SeededRuns.Log.LogInfo($"OnMainMenuManagerStartCalled({__instance.GetHashCode()})");

        UiManager.FindButtonsInMainMenu(__instance);
    }
}

internal static class UiManager
{
    private static ManualLogSource Logger => SeededRuns.Log;

    public static UIBase UiBase { get; private set; }

    public static SeedPanel? SeedPanel { get; set; }

    internal static Button? startRogModeButton;
    internal static Button? startSurvivorsModeButton;

    private static Button.ButtonClickedEvent startRogModeButtonClickedEvent;
    private static Button.ButtonClickedEvent startSurvivorsModeButtonClickedEvent;

    internal static void Initialize()
    {
        const float startupDelay = 2f;
        UniverseLib.Config.UniverseLibConfig config = new()
        {
            Disable_EventSystem_Override = false, // or null
            Force_Unlock_Mouse = false, // or null
            Allow_UI_Selection_Outside_UIBase = true,
            Unhollowed_Modules_Folder = Path.Combine(BepInEx.Paths.BepInExRootPath, "interop") // or null
        };
        
        Universe.Init(startupDelay, OnInitialized, LogHandler, config);

    }

    static void OnInitialized()
    {
        UiBase = UniversalUI.RegisterUI(MyPluginInfo.PLUGIN_GUID, UiUpdate);
    }

    static void LogHandler(string message, LogType type)
    {
        Logger.LogInfo(message);
    }

    internal static void CreateAllPanels()
    {
        CreateSeedPanel();
    }

    private static void CreateSeedPanel()
    {
        SeedPanel = new SeedPanel(UiBase);
        SeedPanel.SetActive(false);
    }

    public static void ShowSeedPanel()
    {
        if (SeedPanel == null) CreateSeedPanel();
        SeedPanel.SetActive(true);
    }

    static void UiUpdate()
    {
        // Called once per frame when your UI is being displayed.
    }

    public static void HideSeedPanel()
    {
        SeedPanel.SetActive(false);
    }

    public static void FindButtonsInMainMenu(MainMenuManager mainMenuManager)
    {
        mainMenuManager.StartCoroutine(FindButtons(mainMenuManager));
    }

    private static IEnumerator FindButtons(MainMenuManager mainMenuManager)
    {
        while (startRogModeButton == null || startSurvivorsModeButton == null)
        {
            if (mainMenuManager.RogModeNewGameButton != null)
            {
                startRogModeButton = mainMenuManager.RogModeNewGameButton.GetComponent<Button>();
                SeededRuns.Log.LogInfo($"Found startRogModeButton: {startRogModeButton}");
                
                startRogModeButtonClickedEvent = startRogModeButton.onClick;
                startRogModeButton.onClick = new Button.ButtonClickedEvent();
                startRogModeButton.onClick.AddListener(ShowSeedPanel);
            }

            if (mainMenuManager.SurvivorsModeNewGameButton != null)
            {
                startSurvivorsModeButton = mainMenuManager.SurvivorsModeNewGameButton.GetComponent<Button>();
                SeededRuns.Log.LogInfo($"Found startSurvivorsModeButton: {startSurvivorsModeButton}");
                
                startSurvivorsModeButtonClickedEvent = startSurvivorsModeButton.onClick;
                startSurvivorsModeButton.onClick = new Button.ButtonClickedEvent();
                startSurvivorsModeButton.onClick.AddListener(ShowSeedPanel);
            }

            yield return new WaitForSeconds(0.1f);
        }
        
        
    }
}