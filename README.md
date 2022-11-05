# Rogue Genesia Mod Manager

A simple mod that adds a button to the main menu and displays a list of loaded mods. More to come in the future.

### How to add support for ModManager to your mod?

ModManager will detect your mod automatically and display the name, version and a description placeholder without doing anything.

However, it can pull some additional information from your mod. To enable that functionality, do the following:

First, add `Path to Rogue Genesia game folder\BepInEx\plugins\ModManager\ModManager.RogueGenesiaMod.dll` as a reference to your project.

Then, instead of extending `BasePlugin`, extend `RogueGenesiaMod` and you will have additional functionality available:

```cs
using BepInEx;
using BepInEx.Unity.IL2CPP;

namespace MyMod
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class MyMod : RogueGenesiaMod
    {
        // The description that will be displayed for your mod in ModManager.
        // This can be dynamically generated, as it is only called when the ModManager window displays.
        public override string ModDescription() => "My awesome mod description";

        // If you return true here, if you want ModManager to display a small '?' button next to your mod info
        public override bool SupportsDetailButtonClick() => false;

        // This method will be called when the small '?' button for your mod is clicked. You can do
        // whatever you want in here. This could be a UI specific to your mod.
        public override void OnDetailButtonClicked(GameObject modManagerDialog)
        {
            Log.LogInfo("Detail button clicked");
        }
    }
}
```