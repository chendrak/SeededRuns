using System.Collections;
using HarmonyLib;
using RogueGenesia.UI;
using UnityEngine;
using UnityEngine.UI;
using BepInEx.Unity.IL2CPP.Utils;
using RogueGenesia.Actors.Map;
using SeededRuns.UI;

namespace SeededRuns.Helpers;

[HarmonyPatch]
internal static class UiElementLocatorStartGameHooks
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public static void OnMainMenuManagerStartCalled(MainMenuManager __instance)
    {
        UiElementLocator.FindButtonsInMainMenu(__instance);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(StageSelectionManager), nameof(StageSelectionManager.Start))]
    public static void OnStageSelectionManagerStartCalled(StageSelectionManager __instance)
    {
        UiElementLocator.FindStageTextInStageSelectionManager(__instance);
    }
}

public static class UiElementLocator
{
    private static Button? startRogModeButton;
    private static Button? startSurvivorsModeButton;
    private static Text? stageText;

    public static void FindButtonsInMainMenu(MainMenuManager mainMenuManager)
    {
        startRogModeButton = null;
        startSurvivorsModeButton = null;
        mainMenuManager.StartCoroutine(FindButtons(mainMenuManager));
    }

    private static IEnumerator FindButtons(MainMenuManager mainMenuManager)
    {
        while (startRogModeButton == null || startSurvivorsModeButton == null)
        {
            if (mainMenuManager.RogModeNewGameButton != null)
            {
                startRogModeButton = mainMenuManager.RogModeNewGameButton.GetComponent<Button>();
                SeededRuns.Log.LogDebug($"Found startRogModeButton: {startRogModeButton}");
                SeedPanelController.SetRogButton(startRogModeButton);
            }

            if (mainMenuManager.SurvivorsModeNewGameButton != null)
            {
                startSurvivorsModeButton = mainMenuManager.SurvivorsModeNewGameButton.GetComponent<Button>();
                SeededRuns.Log.LogDebug($"Found startSurvivorsModeButton: {startSurvivorsModeButton}");
                SeedPanelController.SetSurvivorsModeButton(startSurvivorsModeButton);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public static void FindStageTextInStageSelectionManager(StageSelectionManager stageSelectionManager)
    {
        stageText = null;
        stageSelectionManager.StartCoroutine(FindStageText(stageSelectionManager));
    }

    private static IEnumerator FindStageText(StageSelectionManager stageSelectionManager)
    {
        while (stageText == null)
        {
            if (stageSelectionManager.stageText != null)
            {
                stageText = stageSelectionManager.stageText;
                SeedOnMapController.OnStageTextFound(stageText);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}