using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class UnitMovement
{
    private Queue<Vector3> m_path;
    private Transform m_transform;
    private float m_speed;
    public event Action PathComplete;
    [SerializeField] private List<Vector3> debugPaths;

    [SerializeField] private int current;

    public UnitMovement(MonoBehaviour owner, float speed)
    {
        if (owner == null) throw new ArgumentException("Owner is null", nameof(owner));
        m_path = new();
        debugPaths = new(m_path);
        m_transform = owner.transform;
        m_speed = speed;
        owner.StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        GeneratePaths();
        while (m_path.Count > 0)
        {
            current++;
            var endValue = m_path.Dequeue();
            var duration = Vector3.Distance(m_transform.position, endValue) / m_speed;
            yield return m_transform.DOMove(endValue, duration).WaitForCompletion();
        }
        PathComplete?.Invoke();
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
