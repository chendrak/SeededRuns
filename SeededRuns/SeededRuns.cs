using System;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ModManager;
using SeededRuns.Helpers;
using SeededRuns.UI;

namespace SeededRuns
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class SeededRuns : RogueGenesiaMod
    {
        public static ManualLogSource Log;
        private static Harmony _harmony;

        public override void Load()
        {
            Log = base.Log;
            Log.LogInfo($"SeededRuns.Load");
            _harmony = new Harmony($"{MyPluginInfo.PLUGIN_GUID}-{Guid.NewGuid()}");
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            
            ConfigHelper.Init(Config);
            UiManager.Initialize();
        }

        public override bool Unload()
        {
            Log.LogInfo($"SeededRuns.Unload");
            _harmony.UnpatchSelf();
            Log.LogInfo($"Patched methods: {_harmony.GetPatchedMethods().Count()}");
            UiManager.Deinitialize();
            return true;
        }

        public override string ModDescription() => "Make runs repeatable";
    }
}