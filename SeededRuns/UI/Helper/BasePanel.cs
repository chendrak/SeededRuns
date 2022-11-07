using SeededRuns.Extensions;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;

namespace SeededRuns.UI.Helper;

public abstract class BasePanel : UniverseLib.UI.Panels.PanelBase
{
    public override int MinWidth { get; }
    public override int MinHeight { get; }
    public override Vector2 DefaultAnchorMin => new(0.5f, 1f);
    public override Vector2 DefaultAnchorMax => new(0.5f, 1f);
    public Text? TitleText => TitleBar.GetChildComponentByName<Text>("TitleBar");

    // Center on screen by default
    public override Vector2 DefaultPosition => new(0 - MinWidth / 2, 0 + MinHeight / 2);

    protected BasePanel(UIBase owner, int minWidth = 320, int minHeight = 240) : base(owner)
    {
        MinWidth = minWidth;
        MinHeight = minHeight;
    }

    private void RemoveCloseButton()
    {
        var closeHolder = GameObject.Find("CloseHolder");
        if (closeHolder)
        {
            Object.Destroy(closeHolder);
        }
    }
    
    protected override void ConstructPanelContent()
    {
        RemoveCloseButton();
    }

    public VerticalLayoutGroup AddVerticalLayoutGroup(GameObject parent, string name, int? padLeft = null, int? padTop = null, int? padRight = null, int? padBottom = null, int? spacing = null, bool autoExpand = true)
    {
        var verticalGroupHolder = UIFactory.CreateUIObject(name, parent);
        var result = UIFactory.SetLayoutGroup<VerticalLayoutGroup>(
            gameObject: verticalGroupHolder,
            padLeft: padLeft,
            padTop: padTop,
            padRight: padRight,
            padBottom: padBottom,
            spacing: spacing
            );

        if (autoExpand)
        {
            var contentSizeFitter = verticalGroupHolder.AddComponent<ContentSizeFitter>();
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
        
        return result;
    }

    public HorizontalLayoutGroup AddHorizontalLayoutGroup(GameObject parent, string name, int? padLeft = null, int? padTop = null, int? padRight = null, int? padBottom = null, int? spacing = null, bool autoExpand = false)
    {
        var horizontalGroupHolder = UIFactory.CreateUIObject(name, parent);
        var result = UIFactory.SetLayoutGroup<HorizontalLayoutGroup>(
            gameObject: horizontalGroupHolder,
            padLeft: padLeft,
            padTop: padTop,
            padRight: padRight,
            padBottom: padBottom,
            spacing: spacing
        );

        if (autoExpand)
        {
            var contentSizeFitter = horizontalGroupHolder.AddComponent<ContentSizeFitter>();
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        return result;
    }

    public void SetBackgroundSprite(Sprite sprite, Image.Type imageType = Image.Type.Sliced) =>
        ContentRoot.SetBackgroundSprite(sprite, imageType);

    public void RemoveBackground() => ContentRoot.RemoveBackground();
}