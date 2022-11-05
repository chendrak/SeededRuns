using BepInEx.Configuration;

namespace SeededRuns.Helpers;

public static class ConfigHelper
{
    private static ConfigFile _configFile;
    public static ConfigEntry<int> configSeed;

    public static void Init(ConfigFile config)
    {
        _configFile = config;
        configSeed = _configFile.Bind(
            "Seeds", "LastUsedSeed", -1, "A user selected seed that is used in all runs");
    }

    public static void UpdateSeed(int seed)
    {
        configSeed.Value = seed;
        _configFile.Save();
    }

    public static int GetLastUsedSeed() => configSeed.Value;
}