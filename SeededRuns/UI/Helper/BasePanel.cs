using BepInEx.Logging;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using Object = UnityEngine.Object;
using System.Collections;

namespace SeededRuns.UI.Helper;

public abstract class BasePanel : UniverseLib.UI.Panels.PanelBase
{
    protected static ManualLogSource Log = SeededRuns.Log;

    public override int MinWidth { get; }
    public override int MinHeight { get; }
    public override Vector2 DefaultAnchorMin => new(0.5f, 0.5f);
    public override Vector2 DefaultAnchorMax => new(0.5f, 0.5f);
    public override bool CanDragAndResize => false;
    protected virtual bool ShouldRemoveWindowBackground => true;

    // Center on screen by default
    public override Vector2 DefaultPosition => new(0 - Width / 2, 0 + Height / 2);
    
    public float Width => Rect.sizeDelta.x;
    public float Height => Rect.sizeDelta.y;

    
    public Text TitleText { get; private set; }

    protected BasePanel(UIBase owner, int minWidth = 320, int minHeight = 240, bool autoExpandVertical = false,
        bool autoExpandHorizontal = false) : base(owner)
    {
        MinWidth = minWidth;
        MinHeight = minHeight;

        if (autoExpandHorizontal || autoExpandVertical)
        {
            var csf = uiRoot.AddComponent<ContentSizeFitter>();

            if (autoExpandHorizontal)
            {
                csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            }

            if (autoExpandVertical)
            {
                csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }
    }

    private void CreateTitleText(string text)
    {
        TitleText = ContentRoot.AddLabel(Name, text);
        TitleText.SetDimensions(minWidth: 10, flexibleWidth: 9999);
        TitleText.SetActive(false);
    }

    public void SetTitle(string title)
    {
        TitleText.text = title;
    }

    public void SetTitleAlignment(TextAnchor alignment)
    {
        TitleText.alignment = alignment;
    }

    public void ShowTitle() => TitleText.SetActive(true);
    public void HideTitle() => TitleText.SetActive(false);

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
        if (ShouldRemoveWindowBackground)
        {
            UIRoot.RemoveBackground();
        }

        TitleBar.RemoveBackground();
        TitleBar.SetActive(false);
        CreateTitleText(Name);
    }

    public VerticalLayoutGroup AddVerticalLayoutGroup(GameObject parent, string name, int? padLeft = null,
        int? padTop = null, int? padRight = null, int? padBottom = null, int? spacing = null, bool autoExpand = true)
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
            var contentSizeFitter = parent.AddComponent<ContentSizeFitter>();
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        return result;
    }

    public HorizontalLayoutGroup AddHorizontalLayoutGroup(GameObject parent, string name, int? padLeft = null,
        int? padTop = null, int? padRight = null, int? padBottom = null, int? spacing = null, bool autoExpand = false)
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
            var contentSizeFitter = parent.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        return result;
    }

    public void SetBackgroundSprite(Sprite sprite, Image.Type imageType = Image.Type.Sliced) =>
        ContentRoot.SetBackgroundSprite(sprite, imageType);

    public void RemoveBackground() => ContentRoot.RemoveBackground();

    private HorizontalOrVerticalLayoutGroup? GetLayoutGroup()
    {
        HorizontalOrVerticalLayoutGroup layoutGroup;

        layoutGroup = ContentRoot.GetComponent<VerticalLayoutGroup>();
        if (layoutGroup == null)
        {
            layoutGroup = ContentRoot.GetComponent<HorizontalLayoutGroup>();
        }

        return layoutGroup;
    }

    public void SetPadding(int? paddingLeft = null,
        int? paddingTop = null, int? paddingRight = null,
        int? paddingBottom = null)
    {
        GetLayoutGroup()?.SetPadding(paddingLeft, paddingTop, paddingRight, paddingBottom);
    }

    public void SetPadding(int padding)
    {
        GetLayoutGroup()?.SetPadding(padding, padding, padding, padding);
    }

    public void SetSpacing(int spacing)
    {
        GetLayoutGroup()?.SetSpacing(spacing);
    }

    public void CenterOnScreen()
    {
        Vector3 pos = this.Rect.position;
        Vector3 panelDimensions = this.Rect.sizeDelta;
        Vector2 screenDimensions = new(Screen.width, Screen.height);

        pos.x = (screenDimensions.x - panelDimensions.x) / 2;
        pos.y = (screenDimensions.y + panelDimensions.y) / 2;

        this.Rect.position = pos;
    }
}