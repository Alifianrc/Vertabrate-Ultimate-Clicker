using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Subtitle;
    [SerializeField] private Image Image;

    [SerializeField] private TextMeshProUGUI DescText;

    [SerializeField] private ScrollRect ScrollRect;

    public void SetData(JSONReader.VertebrateData data)
    {
        Title.text = data.Name;
        Subtitle.text = data.LatinName;
        DescText.text = data.Description;
        Image.sprite = JSONReader.Instance.GetVertebrateImage(data.Name);
        ScrollRect.normalizedPosition = Vector2.one;
    }
}
