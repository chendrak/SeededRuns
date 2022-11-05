#nullable enable
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib;
using UniverseLib.UI;
using UniverseLib.Utility;

namespace SeededRuns.Helpers;

public static class GameResources
{
    private static T? GetResourceByName<T>(string resourceName) where T : Object
    {
        var objects = RuntimeHelper.FindObjectsOfTypeAll<T>();
        return objects?.FirstOrDefault(obj => obj.name == resourceName);
    }

    public static Color DefaultGray = "#848284".ToColor();

    private static SpriteState? _defaultSpriteState;
    public static SpriteState DefaultSpriteState
    {
        get
        {
            if (_defaultSpriteState == null)
            {
                _defaultSpriteState = new SpriteState
                {
                    selectedSprite = Resources.Load<Sprite>("UI/UIElements/Button_Selected"),
                    highlightedSprite = Resources.Load<Sprite>("UI/UIElements/Button_Selected"),
                    disabledSprite = Resources.Load<Sprite>("UI/UIElements/Button_Disabled")
                };
            }
            
            return _defaultSpriteState;
        }
    }

    private static Sprite? _menuLine;
    public static Sprite MenuLine
    {
        get
        {
            if (_menuLine == null)
            {
                _menuLine = Resources.Load<Sprite>("UI/UIElements/Menu_Line");
            }
            
            return _menuLine;
        }
    }
    
    private static Sprite? _defaultButtonSprite;
    public static Sprite DefaultButtonSprite
    {
        get
        {
            if (_defaultButtonSprite == null)
            {
                _defaultButtonSprite = Resources.Load<Sprite>("UI/UIElements/Button_Default_State");
            }
            
            return _defaultButtonSprite;
        }
    }
    
    private static Font? _pixelFont;
    public static Font PixelFont
    {
        get
        {
            if (_pixelFont == null)
            {
                _pixelFont = GetResourceByName<Font>("pixelplay") ?? UniversalUI.DefaultFont;
            }
            
            return _pixelFont;
        }
    }
}