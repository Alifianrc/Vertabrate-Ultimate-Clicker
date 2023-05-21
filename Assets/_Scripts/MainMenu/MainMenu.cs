using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Object in this main menu group
    [SerializeField] private RectTransform MainMenuPanel;
    [SerializeField] private Button MainMenuButton;

    [SerializeField] private RectTransform MenuPanelGroup;
    [SerializeField] private RectTransform StatsPanel;
    [SerializeField] private RectTransform AchievementPanel;
    [SerializeField] private RectTransform DictionaryPanel;

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
    private float MainMenuPanelHeight => MainMenuPanel.rect.height;
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
        float targetY;

        if (m_mainMenuIsOpen)
        {
            targetY = MainMenuPanel.position.y + (MainMenuPanelHeight * 2) - MainMenuButtonHeight;
            MainMenuPanel.DOMoveY(targetY, duration);
        }
        else
        {
            targetY = MainMenuPanel.position.y - (MainMenuPanelHeight * 2) + MainMenuButtonHeight;
            MainMenuPanel.DOMoveY(targetY, duration);
        }
    }

    // Slide Panel Menu -------------------------------------------------------------------------------
    private float SubMenuPanelWidth => StatsPanel.GetComponent<RectTransform>().rect.width;
    private const float SubMenuPanelMargin = 20.0f;
    private int m_subMenuPanelIndex = 0;
    public void GoToMenuPanel(int index)
    {
        // Checking index value
        if (m_subMenuPanelIndex == index) return;
        if(index < 0 || index > 2)
        {
            index = 0;
            if(m_subMenuPanelIndex == index)
            {
                return;
            }
        }
        m_subMenuPanelIndex = index;
        
        // Translate rectTransform to transform
        var xRectPos = -(SubMenuPanelWidth + SubMenuPanelMargin) * index;
        var transformPosition = transform.TransformPoint(new Vector3(xRectPos, 0, 0));
        MenuPanelGroup.transform.DOMoveX(transformPosition.x, Slide_Duration);
    }
}
