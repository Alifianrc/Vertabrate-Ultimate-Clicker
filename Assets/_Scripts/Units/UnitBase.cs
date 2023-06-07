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
    public Stats Stats => m_stats;
    public bool IsInitialized { get; private set; }

    private void Start()
    {
        Play(AnimationType.Move);
    }

    public void Init(Stats stats, List<AnimationSheet> animations)
    {
        if(IsInitialized) return;
        
        m_animationHandler = new(animations, GetComponent<SpriteRenderer>());
        m_stats = stats;
        m_movement = new(this, stats.Speed);
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
        m_movement.Stop();
    }

    public void Play(AnimationType type) => m_animationHandler.Play(type);
    public void Stop() => m_animationHandler.Stop();

    public virtual void TakeDamage(int dmg)
    {
        m_stats.Health -= dmg;
        m_animationHandler.PlayTemporary(AnimationType.Hurt, 1f);
    }
}