using System.Collections;
using HarmonyLib;
using RogueGenesia.UI;
using UnityEngine;
using UnityEngine.UI;
using BepInEx.Unity.IL2CPP.Utils;
using SeededRuns.UI;

namespace SeededRuns.Helpers;

[HarmonyPatch]
internal static class UiElementLocatorStartGameHooks
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public static void OnMainMenuManagerStartCalled(MainMenuManager __instance)
    {
        SeededRuns.Log.LogDebug($"OnMainMenuManagerStartCalled({__instance.GetHashCode()})");
        UiElementLocator.FindButtonsInMainMenu(__instance);
    }
}

public static class UiElementLocator
{
    internal static Button? startRogModeButton;
    internal static Button? startSurvivorsModeButton;

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
}