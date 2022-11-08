using SeededRuns.UI;
using SeededRuns.UI.Helper;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using UniverseLib.UI.Models;

namespace SeededRuns.Helpers;

public class MessageBox : BasePanel
{
    private const int TitleFontSize = 32;
    private const int CloseButtonFontSize = 20;
    private const int PaddingHorizontal = 16;
    private const int PaddingTop = 8;
    private const int PaddingBottom = 16;
    private const int ElementSpacing = 16;

    private static MessageBox? Instance { get; set; }
    
    public override Vector2 DefaultAnchorMin => new(0.5f, 1f);
    public override Vector2 DefaultAnchorMax => new(0.5f, 1f);

    public override Vector2 DefaultPosition => new(0 - MinWidth / 2, 0 + MinHeight / 2);

    public override bool CanDragAndResize => false;

    protected override bool ShouldRemoveWindowBackground => false;

    private Text MessageText { get; set; }
    private ButtonRef CloseBtn { get; set; }

    public override string Name => "MessageBox";

    public static void Show(string text, string title = "Message")
    {
        if (Instance == null)
        {
            Instance = new MessageBox(UiManager.UiBase);
        }

        Instance.TitleText.text = title;
        Instance.MessageText.text = text;
        Instance.SetActive(true);
    }

    private MessageBox(UIBase owner, int minWidth = 320, int minHeight = 240, bool autoExpandVertical = true, bool autoExpandHorizontal = false) : base(owner, minWidth, minHeight, autoExpandVertical, autoExpandHorizontal)
    {
        Instance = this;
    }

    protected override void ConstructPanelContent()
    {
        base.ConstructPanelContent();
        
        SetBackgroundSprite(GameResources.DefaultButtonSprite);
        SetPadding(PaddingHorizontal, PaddingTop, PaddingHorizontal, PaddingBottom);
        SetSpacing(ElementSpacing);
        
        #region Title
        TitleText.font = GameResources.PixelFont;
        TitleText.fontSize = TitleFontSize;

        SetTitleAlignment(TextAnchor.MiddleCenter);
        ShowTitle();
        #endregion

        MessageText = ContentRoot.AddLabel(
            name: "MessageBoxLabel", text: "",
            font: GameResources.PixelFont, fontSize: CloseButtonFontSize,
            textColor: Color.white
        );
        
        CloseBtn = ContentRoot.AddButton("CloseBtn", "OK", GameResources.PixelFont, CloseButtonFontSize);
        CloseBtn.OnClick += OnClosePanelClicked;
    }
}