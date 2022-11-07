using SeededRuns.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace SeededRuns.UI.Helper;

public static class GameObjectExtensions
{
    public static bool RemoveBackground(this GameObject obj)
    {
        var backgroundImage = obj.GetComponent<Image>();
        if (!backgroundImage) return false;
        
        Object.Destroy(backgroundImage);
        return true;
    }

    public static void SetBackgroundSprite(this GameObject obj, Sprite sprite, Image.Type imageType = Image.Type.Sliced)
    {
        var backgroundImage = obj.GetOrAddComponent<Image>();
        backgroundImage.sprite = sprite;
        backgroundImage.type = imageType;
        backgroundImage.color = Color.clear;
    }

    public static void SetBackgroundColor(this GameObject obj, Color color)
    {
        var backgroundImage = obj.GetOrAddComponent<Image>();
        backgroundImage.sprite = null;
        backgroundImage.color = color;
    }
}