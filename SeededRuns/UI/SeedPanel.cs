using BepInEx.Logging;
using SeededRuns.Helpers;
using SeededRuns.UI.Extensions;
using SeededRuns.UI.Helper;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using UniverseLib.UI.Models;
using Image = UnityEngine.UI.Image;

namespace SeededRuns.UI;

public class SeedPanel : BasePanel
{
    private const int TitleFontSize = 32;
    private const int CloseButtonFontSize = 20;
    private const int PaddingHorizontal = 16;
    private const int PaddingTop = 8;
    private const int PaddingBottom = 16;
    private const int ElementSpacing = 8;
    private Color DefaultButtonColor = new(0.25f, 0.25f, 0.25f);

    public override string Name => "Seed";
    
    private Button.ButtonClickedEvent _originalClickedEvent;
    
    public override bool CanDragAndResize => false;

    private InputFieldRef _seedInput;
    
    public ButtonRef StartBtn { get; private set; }
    public ButtonRef RandomSeedBtn { get; private set; }
    public ButtonRef CloseBtn { get; private set; }

    public SeedPanel(UIBase owner) : base(owner, 320, 240, true, true) {}

    protected override void ConstructPanelContent()
    {
        base.ConstructPanelContent();

        SetBackgroundSprite(GameResources.DefaultButtonSprite);
        SetPadding(PaddingHorizontal, PaddingTop, PaddingHorizontal, PaddingBottom);
        SetSpacing(ElementSpacing);
        #region TitleArea

        ConfigureTitle();

        var menuLine = UIFactory.CreateUIObject("MenuLine", ContentRoot);
        var img = menuLine.AddComponent<Image>();
        img.color = GameResources.DefaultGray;
        UIFactory.SetLayoutElement(menuLine, minHeight: 2, flexibleWidth: 9999);

        #endregion

        #region Content

        // Label
        var seedLabel = ContentRoot.AddLabel(
            name: "SeedLabel", text: "Enter your seed:",
            font: GameResources.PixelFont, fontSize: CloseButtonFontSize,
            textColor: Color.white
        );
        
        // Input
        _seedInput = ContentRoot.AddInputField("SeedInput", placeholder: "-1",
            content: ConfigHelper.GetLastUsedSeed().ToString(),
            contentType: InputField.ContentType.IntegerNumber, font: GameResources.PixelFont, fontSize: 16);

        _seedInput.OnValueChanged += OnSeedInputValueChanged;
        
        #endregion

        #region Buttons

        var buttonWrapper = AddHorizontalLayoutGroup(ContentRoot, "ButtonWrapper", spacing: 24);

        StartBtn = buttonWrapper.AddButton("StartButton", "Start", GameResources.PixelFont, CloseButtonFontSize);
        StartBtn.SetColors(DefaultButtonColor);
        StartBtn.OnClick += OnStartButtonClicked;

        RandomSeedBtn =
            buttonWrapper.AddButton("RandomSeedButton", "Random", GameResources.PixelFont, CloseButtonFontSize);
        RandomSeedBtn.SetColors(DefaultButtonColor);
        RandomSeedBtn.OnClick += OnRandomButtonClicked;
        
        CloseBtn = buttonWrapper.AddButton("CloseBtn", "Cancel", GameResources.PixelFont, CloseButtonFontSize);
        CloseBtn.SetColors(DefaultButtonColor);
        CloseBtn.OnClick += SeedPanelController.HideSeedPanel;
        #endregion
    }

    private void OnSeedInputValueChanged(string text)
    {
        
    }

    private void OnStartButtonClicked()
    {
        int parsedSeed;
        if (int.TryParse(_seedInput.Text, out parsedSeed))
        {
            ConfigHelper.UpdateSeed(parsedSeed);
            _originalClickedEvent.Invoke();
            SetActive(false);
        }
        else
        {
            MessageBox.Show($"Not a valid seed! Please choose a value between {int.MinValue} and {int.MaxValue}");
        }
    }

    private void OnRandomButtonClicked()
    {
        ConfigHelper.UpdateSeed(-1);
        _originalClickedEvent.Invoke();
        SetActive(false);
    }

    private void ConfigureTitle()
    {
        TitleText.font = GameResources.PixelFont;
        TitleText.fontSize = TitleFontSize;

        SetTitleAlignment(TextAnchor.MiddleCenter);
        ShowTitle();
    }

    internal void SetButtonClickEvent(Button.ButtonClickedEvent clickedEvent)
    {
        _originalClickedEvent = clickedEvent;
    }
}