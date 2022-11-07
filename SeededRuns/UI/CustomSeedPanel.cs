using BepInEx.Logging;
using SeededRuns.Helpers;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using UniverseLib.UI.Models;
using Image = UnityEngine.UI.Image;

namespace SeededRuns.UI;

public class SeedPanel : UniverseLib.UI.Panels.PanelBase
{
    private const int TitleFontSize = 32;
    private const int CloseButtonFontSize = 20;
    
    private ManualLogSource Logger => SeededRuns.Log;

    public static SeedPanel Instance { get; private set; }
    
    public override string Name => "Seed";
    public override int MinWidth => 320;
    public override int MinHeight => 160;

    public override Vector2 DefaultAnchorMin => new(0.5f, 1f);
    public override Vector2 DefaultAnchorMax => new(0.5f, 1f);

    public override Vector2 DefaultPosition => new(0 - MinWidth / 2, 0 + MinHeight / 2);

    public override bool CanDragAndResize => false;

    public ButtonRef StartBtn { get; private set; }
    public ButtonRef RandomSeedBtn { get; private set; }
    public ButtonRef CloseBtn { get; private set; }

    public SeedPanel(UIBase owner) : base(owner)
    {
        Instance = this;
    }
    
    protected override void ConstructPanelContent()
    {
        #region TitleArea
        GameObject navbarPanel = UIFactory.CreateUIObject("MainNavbar", ContentRoot);
        UIFactory.SetLayoutGroup<VerticalLayoutGroup>(navbarPanel, true, false, true, true, 2, 5, 5, 6, 6,
            TextAnchor.MiddleCenter);

        Text titleText = UIFactory.CreateLabel(navbarPanel.gameObject, "WindowTitle", Name, TextAnchor.MiddleCenter,
            fontSize: TitleFontSize);
        UIFactory.SetLayoutElement(titleText.gameObject, 50, 25, 9999, 0);
        titleText.font = GameResources.PixelFont;

        var menuLine = UIFactory.CreateUIObject("MenuLine", navbarPanel);
        var img = menuLine.AddComponent<Image>();
        img.color = GameResources.DefaultGray;

        UIFactory.SetLayoutElement(menuLine, minHeight: 2, flexibleWidth: 9999);
        #endregion

        #region Content
        // Label
        var seedLabel = UIFactory.CreateLabel(ContentRoot, "SeedLabel", "Enter your seed:", TextAnchor.MiddleCenter, Color.white);
        seedLabel.font = GameResources.PixelFont;
        UIFactory.SetLayoutElement(seedLabel.gameObject, minHeight: 16, flexibleWidth: 9999);

        // Input
        var inputText = UIFactory.CreateInputField(ContentRoot, "SeedInput", "-1");
        inputText.Component.contentType = InputField.ContentType.IntegerNumber;
        inputText.PlaceholderText.font = GameResources.PixelFont;
        inputText.PlaceholderText.fontSize = 16;
        inputText.Component.textComponent.font = GameResources.PixelFont;
        inputText.Component.textComponent.fontSize = 16;
        UIFactory.SetLayoutElement(inputText.GameObject, minHeight: 24, flexibleWidth: 9999);
        #endregion

        // this.RemoveBackgroundFromElements("Content", "Background", "ModList", "Viewport");

        #region Buttons

        var buttonWrapper = UIFactory.CreateUIObject("ButtonWrapper", ContentRoot);
        UIFactory.SetLayoutElement(buttonWrapper, minWidth: 160, minHeight: 40, flexibleWidth: 9999);
        UIFactory.SetLayoutGroup<HorizontalLayoutGroup>(buttonWrapper, forceWidth: true, childControlWidth: true, childControlHeight: false, spacing: 24, padLeft: 16, padRight: 16);
        
        StartBtn = UIFactory.CreateButton(buttonWrapper, "StartButton", "Start");
        StartBtn.ButtonText.font = GameResources.PixelFont;
        StartBtn.ButtonText.fontSize = CloseButtonFontSize;
        
        RandomSeedBtn = UIFactory.CreateButton(buttonWrapper, "RandomSeedButton", "Random");
        RandomSeedBtn.ButtonText.font = GameResources.PixelFont;
        RandomSeedBtn.ButtonText.fontSize = CloseButtonFontSize;
        
        CloseBtn = UIFactory.CreateButton(buttonWrapper, "CloseBtn", "Cancel");
        CloseBtn.ButtonText.font = GameResources.PixelFont;
        CloseBtn.ButtonText.fontSize = CloseButtonFontSize;
        // UIFactory.SetLayoutElement(CloseBtn.Component.gameObject, minWidth: 160, minHeight: 40, preferredWidth: 160,
        //     preferredHeight: 40, flexibleWidth: 9999, flexibleHeight: 0);

        CloseBtn.OnClick += UiManager.HideSeedPanel;

        // UiHelper.SetBackgroundSprite(UIRoot, GameResources.DefaultButtonSprite);
        // CloseBtn.Component.image.sprite = GameResources.DefaultButtonSprite;
        // CloseBtn.Component.spriteState = GameResources.DefaultSpriteState;
        #endregion
    }
}