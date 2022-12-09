using UnityEngine.UI;
using UniverseLib;

namespace SeededRuns.UI;

public static class SeedPanelController
{
    private static SeedPanel? SeedPanel { get; set; }
    internal static Button? startGameButton;

    private static Button.ButtonClickedEvent startGameButtonClickedEvent;
    
    public static void ShowSeedPanel()
    {
        ShowPanel(startGameButtonClickedEvent);
    }

    private static void ShowPanel(Button.ButtonClickedEvent clickedEvent)
    {
        if (SeedPanel == null) CreateSeedPanel();
        SeedPanel.SetButtonClickEvent(clickedEvent);
        SeedPanel.SetActive(true);
    }
    
    private static void CreateSeedPanel()
    {
        SeedPanel = new SeedPanel(UiManager.UiBase);
        SeedPanel.SetActive(false);
    }

    public static void HideSeedPanel()
    {
        SeedPanel.SetActive(false);
    }

    public static void SetStartGameButton(Button button)
    {
        startGameButton = button;
        startGameButtonClickedEvent = button.onClick;
        startGameButton.onClick = new Button.ButtonClickedEvent();
        startGameButton.onClick.AddListener(ShowSeedPanel);
    }
}