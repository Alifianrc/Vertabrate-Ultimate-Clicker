using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitMovement : MonoBehaviour
{
    private Queue<Vector3> m_path = new();

    private void Start()
    {
        GeneratePaths();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (m_path.Count > 0)
        {
            yield return transform.DOMove(m_path.Dequeue(), 2);
        }
    }

    private void GeneratePaths()
    {
        var count = Random.Range(0, 100);
        while (count-- > 0)
        {
            m_path.Enqueue(PreyManager.Instance.RandomPositionWithin());
        }
    }
}
