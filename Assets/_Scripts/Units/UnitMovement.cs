using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitMovement
{
    private readonly Queue<Vector3> m_path;
    private readonly Transform m_transform;
    private readonly float m_speed;
    private Tween m_tween;
    public bool IsMoving { get; private set; }
    public event Action PathComplete;

    public UnitMovement(Transform owner, float speed)
    {
        if (owner == null) throw new ArgumentException("Owner is null", nameof(owner.gameObject));
        m_path = new();
        m_transform = owner;
        m_speed = speed;
        Start();
    }

    private IEnumerator Move()
    {
        while (m_path.Count > 0)
        {
            yield return new WaitUntil(() => IsMoving);
            var endValue = m_path.Dequeue();
            var duration = Vector3.Distance(m_transform.position, endValue) / m_speed;
            var direction = m_transform.position - endValue;
            m_transform.DOScaleX(direction.x > 0 ? 1 : -1, 0.5f);
            yield return (m_tween = m_transform.DOMove(endValue, duration)).WaitForCompletion();
        }
        PathComplete?.Invoke();
    }

    public void Start()
    {
        if(m_path.Count <= 0)
        {
            GeneratePaths();
            MonoHelper.Instance.Run(Move());
        }
        IsMoving = true;
    }

    public void Stop()
    {
        IsMoving = false;
        m_tween.Kill();
    }

    public void StopTemporary(float time)
    {
        if(!IsMoving) return;
        Stop();
        MonoHelper.Instance.Run(Start, time);
    }

    private void GeneratePaths()
    {
        var count = Random.Range(1, 10);
        while (count-- > 0)
        {
            m_path.Enqueue(PreyManager.Instance.RandomPositionWithin());
        }
    }
}
