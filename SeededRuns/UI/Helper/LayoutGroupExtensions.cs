using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI.Models;

namespace SeededRuns.UI.Helper;

public static class LayoutGroupExtensions
{
    public static ButtonRef AddButton(this HorizontalOrVerticalLayoutGroup group, string name, string text, Font? font = null, int? fontSize = null)
    {
        return group.gameObject.AddButton(name, text, font, fontSize);
    }

    public static void SetSpacing(this HorizontalOrVerticalLayoutGroup layoutGroup, int spacing)
    {
        layoutGroup.spacing = spacing;
    }
    
    public static void SetPadding(this HorizontalOrVerticalLayoutGroup layoutGroup, int? paddingLeft = null, int? paddingTop = null, int? paddingRight = null,
        int? paddingBottom = null)
    {
        var padding = layoutGroup.padding;

        if (paddingLeft.HasValue)
        {
            padding.left = (int)paddingLeft;
        }

        if (paddingTop.HasValue)
        {
            padding.top = (int)paddingTop;
        }

        if (paddingRight.HasValue)
        {
            padding.right = (int)paddingRight;
        }

        if (paddingBottom.HasValue)
        {
            padding.bottom = (int)paddingBottom;
        }
    }
}