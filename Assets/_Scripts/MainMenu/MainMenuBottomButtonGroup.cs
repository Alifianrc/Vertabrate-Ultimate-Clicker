using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBottomButtonGroup : Singleton<MainMenuBottomButtonGroup>
{
    [SerializeField] private GameObject[] BottomButtons;
    [SerializeField] private Color NormalColor;
    [SerializeField] private Color SelectedColor;

    private void Start()
    {
        SetSelectedButton(0);
    }

    public void SetSelectedButton(int index)
    {
        for (int i = 0; i < BottomButtons.Length; i++)
        {
            if(i == index)
            {
                BottomButtons[i].GetComponent<Image>().color = SelectedColor;
            }
            else
            {
                BottomButtons[i].GetComponent<Image>().color = NormalColor;
            }
        }
    }
}
