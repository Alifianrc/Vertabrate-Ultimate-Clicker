using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryGroup : MonoBehaviour
{
    [SerializeField] public GameObject[] DictionaryButton;
    [SerializeField] public Image[] DictionaryImage;
    public const int Length = 3;

    public void SetWidth(float width)
    {
        var rectTransform = GetComponent<RectTransform>();
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.x = width;
        rectTransform.sizeDelta = sizeDelta;
    }
}
