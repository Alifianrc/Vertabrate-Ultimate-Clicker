using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This will share logic for any unit on the field. Could be friend or foe, controlled or not.
/// Things like taking damage, dying, animation triggers etc
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public abstract class UnitBase : MonoBehaviour 
{
    [SerializeField] private UnitAnimationHandler m_animationHandler;
    [SerializeField] private UnitMovement m_movement;
    [SerializeField] private Stats m_stats;
    public event Action<UnitBase> Dead;
    public Stats Stats => m_stats;
    public bool IsInitialized { get; private set; }
    public bool IsDead { get; private set; }

    private void Start()
    {
        Play(AnimationType.Move);
    }

    public void Init(Stats stats, List<AnimationSheet> animations)
    {
        if(IsInitialized) return;
        
        m_animationHandler = new(animations, GetComponent<SpriteRenderer>());
        m_stats = stats;
        m_movement = new(transform, stats.Speed);
        m_movement.Start();
        m_movement.PathComplete += MovementOnPathComplete;
        IsInitialized = true;
    }

    private void MovementOnPathComplete()
    {
        StartCoroutine(m_movement.Move());
    }

    private void OnMouseDown()
    {
        TakeDamage(PlayerData.Instance.FinalDamage);
    }

    public void Play(AnimationType type) => m_animationHandler.Play(type);
    public void Stop() => m_animationHandler.Stop();

    public virtual void TakeDamage(int dmg)
    {
        m_stats.Health -= dmg;
        m_movement.Stop();
        m_animationHandler.PlayTemporary(AnimationType.Hurt, 1f);

        if (m_stats.Health <= 0 && !IsDead)
        {
            IsDead = true;
            Dead?.Invoke(this);
        }
    }
}