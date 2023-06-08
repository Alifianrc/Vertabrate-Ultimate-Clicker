using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class UnitMovement
{
    private Queue<Vector3> m_path;
    private Transform m_transform;
    private float m_speed;
    private Tween m_tween;
    public bool IsMoving { get; private set; }
    public event Action PathComplete;

    public UnitMovement(Transform owner, float speed)
    {
        if (owner == null) throw new ArgumentException("Owner is null", nameof(owner.gameObject));
        m_path = new();
        m_transform = owner;
        m_speed = speed;
        GeneratePaths();
        MonoHelper.Instance.Run(Move());
    }

    private IEnumerator Move()
    {
        while (m_path.Count > 0)
        {
            var endValue = m_path.Dequeue();
            var duration = Vector3.Distance(m_transform.position, endValue) / m_speed;
            yield return new WaitUntil(() => IsMoving);
            yield return (m_tween = m_transform.DOMove(endValue, duration)).WaitForCompletion();
        }
        PathComplete?.Invoke();
    }

    public void Start()
    {
        if(m_path.Count <= 0) GeneratePaths();
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
        var count = Random.Range(0, 100);
        while (count-- > 0)
        {
            m_path.Enqueue(PreyManager.Instance.RandomPositionWithin());
        }
    }
}
