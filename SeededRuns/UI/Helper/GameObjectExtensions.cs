using SeededRuns.Extensions;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using UniverseLib.UI.Models;
using Image = UnityEngine.UI.Image;

namespace SeededRuns.UI.Helper;

public static class GameObjectExtensions
{
    public static void SetActive(this MonoBehaviour behaviour, bool active)
    {
        behaviour.gameObject.SetActive(active);
    }

    public static void SetDimensions(this MonoBehaviour behaviour, int? minWidth = null, int? minHeight = null,
        int? preferredWidth = null, int? preferredHeight = null,
        int? flexibleWidth = null, int? flexibleHeight = null
    )
    {
        behaviour.gameObject.SetDimensions(minWidth, minHeight, preferredWidth, preferredHeight, flexibleWidth,
            flexibleHeight);
    }

    public static void SetDimensions(this GameObject obj, int? minWidth = null, int? minHeight = null,
        int? preferredWidth = null, int? preferredHeight = null,
        int? flexibleWidth = null, int? flexibleHeight = null
    )
    {
        UIFactory.SetLayoutElement(obj,
            minWidth, minHeight,
            flexibleWidth, flexibleHeight,
            preferredWidth, preferredHeight
        );
    }

    public static InputFieldRef AddInputField(this GameObject obj, string name, string? content = null,
        string placeholder = "",
        InputField.ContentType? contentType = null, Font? font = null, int? fontSize = null)
    {
        var inputText = UIFactory.CreateInputField(obj, name, placeholder);

        if (contentType.HasValue)
        {
            inputText.Component.contentType = contentType.Value;
        }

        if (content != null)
        {
            inputText.Text = content;
        }

        if (font)
        {
            inputText.PlaceholderText.font = font;
            inputText.Component.textComponent.font = font;
        }

        if (fontSize.HasValue)
        {
            inputText.PlaceholderText.fontSize = fontSize.Value;
            inputText.Component.textComponent.fontSize = fontSize.Value;
        }

        UIFactory.SetLayoutElement(inputText.GameObject, minWidth: 160, minHeight: 40, flexibleWidth: 9999);
        
        return inputText;
    }

    public static Text AddLabel(this GameObject obj, string name, string text, Font? font = null, int? fontSize = null,
        TextAnchor alignment = TextAnchor.MiddleLeft, Color textColor = default)
    {
        var label = UIFactory.CreateLabel(obj, name, text, alignment, textColor);

        if (font)
        {
            label.font = font;
        }

        if (fontSize.HasValue)
        {
            label.fontSize = (int)fontSize;
        }

        return label;
    }

    public static ButtonRef AddButton(this GameObject obj, string name, string text, Font? font = null,
        int? fontSize = null)
    {
        var button = UIFactory.CreateButton(obj, name, text);

        if (font)
        {
            button.ButtonText.font = font;
        }

        if (fontSize.HasValue)
        {
            button.ButtonText.fontSize = (int)fontSize;
        }

        UIFactory.SetLayoutElement(button.GameObject, minWidth: 160, minHeight: 40, flexibleWidth: 9999);

        return button;
    }

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
        backgroundImage.color = Color.white;
    }

    public static void SetBackgroundColor(this GameObject obj, Color color)
    {
        var backgroundImage = obj.GetOrAddComponent<Image>();
        backgroundImage.sprite = null;
        backgroundImage.color = color;
    }
}