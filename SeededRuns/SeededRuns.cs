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

        public override void Load()
        {
            Log = base.Log;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            ConfigHelper.Init(Config);
            UiManager.Initialize();
        }

        public override string ModDescription() => "Make runs repeatable";
    }
}