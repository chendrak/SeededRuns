using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI.Panels;

namespace SeededRuns.UI.Extensions;

public static class PanelExtensions
{
    public static bool SetBackgroundColor(this PanelBase panel, Color color)
    {
        var contentBg = panel.ContentRoot.GetComponent<Image>();
        if (contentBg)
        {
            contentBg.color = color;
            return true;
        }
        
        SeededRuns.Log.LogError("Could not find bg image");
        return false;
    }

    public static bool RemoveBackgroundFromElements(this GameObject root, params string[] elementNames)
    {
        var atLeastOneUpdated = false;
        var bgImages = root.GetComponentsInChildren<Image>();
        if (bgImages != null)
        {
            foreach (var bgImage in bgImages)
            {
                if (elementNames.Contains(bgImage.name))
                {
                    Object.Destroy(bgImage);
                    atLeastOneUpdated = true;
                }
            }
        }

        return atLeastOneUpdated;
    }
    
    public static bool RemoveBackgroundFromElements(this PanelBase panel, params string[] elementNames)
    {
        return RemoveBackgroundFromElements(panel.ContentRoot, elementNames);
    }
    
    public static bool SetBackgroundColorForElements(this PanelBase panel, Color color, params string[] elementNames)
    {
        var atLeastOneUpdated = false;
        var bgImages = panel.ContentRoot.GetComponentsInChildren<Image>();
        if (bgImages != null)
        {
            foreach (var bgImage in bgImages)
            {
                if (elementNames.Contains(bgImage.name))
                {
                    bgImage.color = color;
                    atLeastOneUpdated = true;
                }
            }
        }

        return atLeastOneUpdated;
    }
}