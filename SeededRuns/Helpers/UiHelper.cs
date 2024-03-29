using UnityEngine;
namespace SeededRuns.Helpers;

public static class UiHelper
{
    public static string ToHex(Color color)
    {
        byte r = (byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255);
        byte g = (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255);
        byte b = (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255);
        byte a = (byte)Mathf.Clamp(Mathf.RoundToInt(color.a * 255f), 0, 255);

        return $"{r:X2}{g:X2}{b:X2}{a:X2}";
    }
}