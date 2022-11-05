using System.IO;

namespace SeededRuns;

public static class Paths
{
    public static string PluginPath = Path.Combine(BepInEx.Paths.PluginPath, MyPluginInfo.PLUGIN_NAME);
    public static string Assets = Path.Combine(PluginPath ,"Assets");
}