using SeededRuns.UI.Helper;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib;
using UniverseLib.UI.Models;

namespace SeededRuns.UI.Extensions;

public static class ButtonExtensions
{
    public static void SetSprite(this ButtonRef button, Sprite sprite, Image.Type imageType = Image.Type.Sliced)
    {
        button.Component.image.sprite = sprite;
        button.Component.image.type = imageType;
        // RuntimeHelper.SetColorBlockAuto(button.Component, Color.white);
        RuntimeHelper.SetColorBlock(button.Component, Color.white, Color.yellow, Color.blue, Color.gray);
    }

    public static void SetSpriteState(this ButtonRef button, Sprite? highlighted, Sprite? selected, Sprite? pressed, Sprite? disabled)
    {
        if (highlighted || selected || pressed || disabled)
        {
            var spriteState = new SpriteState();

            if (highlighted)
            {
                spriteState.highlightedSprite = highlighted;
            }
            
            if (selected)
            {
                spriteState.selectedSprite = selected;
            }
            
            if (pressed)
            {
                spriteState.pressedSprite = pressed;
            }
            
            if (disabled)
            {
                spriteState.disabledSprite = disabled;
            }
            
            button.Component.spriteState = spriteState;
        }
    }

    public static void RemoveSprite(this ButtonRef button)
    {
        button.Component.image.sprite = null;
        button.Component.spriteState = null;
    }

    public static void SetColors(this ButtonRef button, Color color)
    {
        button.SetColors(color, color * 1.2f, color * 0.7f, color * 0.7f);
    }

    public static void SetColors(this ButtonRef button, Color normal, Color highlighted, Color pressed, Color disabled)
    {
        RuntimeHelper.SetColorBlock(button.Component, normal, highlighted, pressed, disabled);
    }

    public static void SetActive(this ButtonRef button, bool active)
    {
        button.Component.SetActive(active);
    }
}