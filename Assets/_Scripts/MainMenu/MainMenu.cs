using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Obejct in this main menu groub
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject MainMenuButton;

    [SerializeField] private GameObject MenuPanelGroup;
    [SerializeField] private GameObject StatsPanel;
    [SerializeField] private GameObject AchievementPanel;
    [SerializeField] private GameObject DictionaryPanel;

    // Public Variable
    public const float Slide_Duration = 0.1f;

    private void Start()
    {
        // Always close main menu first
        CloseMainMenu(0);
        // Got to stats panel first
        GoToMenuPanel(0);
    }

    // Open Close Main Menu ---------------------------------------------------------------------------
    private float MainMenuPanelHeight => MainMenuPanel.GetComponent<RectTransform>().rect.height;
    private float MainMenuButtonHeight => MainMenuButton.GetComponent<RectTransform>().rect.height;
    private bool m_mainMenuIsOpen = true;
    public void OpenMainMenu(float duration =  Slide_Duration)
    {
        if (m_mainMenuIsOpen) return;
        OpenCloseMainMenu(duration);
    }
    public void CloseMainMenu(float duration = Slide_Duration)
    {
        if (!m_mainMenuIsOpen) return;
        OpenCloseMainMenu(duration);
    }
    public void OpenCloseMainMenu(float duration)
    {
        m_mainMenuIsOpen = !m_mainMenuIsOpen;
        
        if(m_mainMenuIsOpen)
            MainMenuPanel.transform.DOMoveY(MainMenuPanel.transform.position.y + (MainMenuPanelHeight * 2) - MainMenuButtonHeight, duration);
        else
            MainMenuPanel.transform.DOMoveY(MainMenuPanel.transform.position.y - (MainMenuPanelHeight * 2) + MainMenuButtonHeight, duration);
    }

    // Slide Panel Menu -------------------------------------------------------------------------------
    private float SubMenuPanelWidth => StatsPanel.GetComponent<RectTransform>().rect.width;
    private const float SubMenuPanelMargin = 20.0f;
    private int m_subMenupPanelIndex = 0;
    public void GoToMenuPanel(int index)
    {
        if (m_subMenupPanelIndex == index) return;
        if(index < 0 || index > 2)
        {
            index = 0;
            if(m_subMenupPanelIndex == index)
            {
                return;
            }
        }
        m_subMenupPanelIndex = index;
        Debug.Log(-(SubMenuPanelWidth + SubMenuPanelMargin) * index);
        MenuPanelGroup.transform.DOMoveX(-(SubMenuPanelWidth + SubMenuPanelMargin) * index, Slide_Duration);
    }
}
