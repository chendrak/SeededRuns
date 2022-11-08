using SeededRuns.Helpers;
using SeededRuns.UI.Extensions;
using SeededRuns.UI.Helper;
using UnityEngine;
using UniverseLib.UI;

namespace SeededRuns.UI;

public class TestPanel : BasePanel
{
    private const int TitleFontSize = 32;
    private const int DefaultButtonFontSize = 20;
    private const int PaddingHorizontal = 16;
    private const int PaddingVertical = 8;
    public override string Name => "TestPanel";

    public TestPanel(UIBase owner, int minWidth = 20, int minHeight = 20) : base(owner, minWidth, minHeight, true)
    {
    }

    protected override void ConstructPanelContent()
    {
        base.ConstructPanelContent();

        SetBackgroundSprite(GameResources.DefaultButtonSprite);
        SetPadding(PaddingHorizontal, PaddingVertical, PaddingHorizontal, PaddingVertical);

        ConfigureTitle();

        var verticalWrapper = AddVerticalLayoutGroup(ContentRoot, "TestContent",
            padLeft: 24, padRight: 24,
            padTop: 16, padBottom: 16,
            spacing: 8, autoExpand: true
        );

        for (int i = 0; i < 3; i++)
        {
            var btn = verticalWrapper.AddButton($"Button{i}", $"Button {i}", GameResources.PixelFont,
                DefaultButtonFontSize);

            btn.SetSprite(GameResources.DefaultButtonSprite);
        }

        var horizontalWrapper = AddHorizontalLayoutGroup(ContentRoot, "TestHorizontalContent",
            padLeft: 8, padRight: 8,
            padTop: 4, padBottom: 4,
            spacing: 16, autoExpand: false
        );

        for (int i = 0; i < 3; i++)
        {
            var btn = horizontalWrapper.AddButton($"HButton{i}", $"HButton {i}", GameResources.PixelFont,
                DefaultButtonFontSize);
            btn.SetSprite(GameResources.DefaultButtonSprite);
        }
    }

    private void ConfigureTitle()
    {
        TitleText.font = GameResources.PixelFont;
        TitleText.fontSize = TitleFontSize;

        SetTitle("Test Panel");
        SetTitleAlignment(TextAnchor.MiddleCenter);
        ShowTitle();
    }
}