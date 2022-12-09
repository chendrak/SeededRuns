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
public static class UiElementLocatorStartGameHooks
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(StartGameManager), nameof(StartGameManager.Start))]
    public static void OnMainMenuManagerStartCalled(StartGameManager __instance)
    {
        UiElementLocator.FindButtonsInStartGameManager(__instance);
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
    private static Button? startGameButton;
    private static Text? stageText;

    public static void FindButtonsInStartGameManager(StartGameManager startGameManager)
    {
        startGameButton = null;
        startGameManager.StartCoroutine(FindButtons(startGameManager));
    }

    private static IEnumerator FindButtons(StartGameManager startGameManager)
    {
        while (startGameButton == null)
        {
            if (startGameManager.PlayButton != null)
            {
                startGameButton = startGameManager.PlayButton.TryCast<Button>();
                SeededRuns.Log.LogInfo($"Found startGameButton: {startGameButton}");
                SeedPanelController.SetStartGameButton(startGameButton);
            }

            yield return new WaitForSeconds(0.2f);
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