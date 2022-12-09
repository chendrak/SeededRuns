#nullable enable
using System.Collections.Generic;
using System.IO;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UniverseLib;
using UniverseLib.UI;

namespace SeededRuns.UI;

internal static class UiManager
{
    private static ManualLogSource Logger => SeededRuns.Log;

    public static UIBase UiBase { get; private set; }

    internal static void Initialize()
    {
        const float startupDelay = 0f;
        UniverseLib.Config.UniverseLibConfig config = new()
        {
            Disable_EventSystem_Override = false, // or null
            Force_Unlock_Mouse = false, // or null
            Allow_UI_Selection_Outside_UIBase = true,
            Unhollowed_Modules_Folder = Path.Combine(BepInEx.Paths.BepInExRootPath, "interop") // or null
        };

        Universe.Init(startupDelay, OnInitialized, LogHandler, config);
    }

    internal static void Deinitialize()
    {
        Logger.LogInfo($"UiManager.Deinitialize");
        var registeredUIs = Traverse.Create(typeof(UniversalUI)).Field<Dictionary<string, UIBase>>("registeredUIs");
        registeredUIs.Value.Remove(MyPluginInfo.PLUGIN_GUID);
        var uiBases = Traverse.Create(typeof(UniversalUI)).Field<List<UIBase>>("uiBases");
        uiBases.Value.Remove(UiBase);
        Object.Destroy(UiBase.RootObject);
        UiBase = null;
    }

    static void OnInitialized()
    {
        Logger.LogInfo($"UiManager.OnInitialized");
        UiBase = UniversalUI.RegisterUI(MyPluginInfo.PLUGIN_GUID, UiUpdate);
    }

    static void LogHandler(string message, LogType type)
    {
        Logger.LogInfo(message);
    }

    public static void CreateTestPanel()
    {
        var testPanel = new TestPanel(UiBase, 640, 400);
    }

    static void UiUpdate()
    {
        // Called once per frame when your UI is being displayed.
    }
}