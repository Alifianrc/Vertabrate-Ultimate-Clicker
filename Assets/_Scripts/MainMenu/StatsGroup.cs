using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGroup : MonoBehaviour
{
    [SerializeField] private GameObject BoxGroup;
    [SerializeField] private Transform FirstStatsBoxPos;
    [SerializeField] private GameObject StatsBoxPrefab;

    public const float StatsBoxMargin = 15.0f;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var newBox = Instantiate(StatsBoxPrefab, BoxGroup.transform);
            var transPoint = (new Vector3(FirstStatsBoxPos.transform.position.x, FirstStatsBoxPos.transform.position.y, 0));
            newBox.transform.position = new Vector2(transPoint.x + ((newBox.GetComponent<RectTransform>().rect.width * 2 + StatsBoxMargin) * i), transPoint.y);
        }
    }
}
