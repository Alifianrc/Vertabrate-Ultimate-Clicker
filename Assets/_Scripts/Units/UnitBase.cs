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
    [SerializeField] private Stats m_stats;
    public Stats Stats => m_stats;
    public bool IsInitialized { get; private set; }

    public void Init(Stats stats, List<AnimationSheet> animations)
    {
        if(IsInitialized) return;
        
        m_animationHandler = new UnitAnimationHandler(animations, GetComponent<SpriteRenderer>());
        m_stats = stats;
        IsInitialized = true;
    }

    public void Play(AnimationType type) => m_animationHandler.Play(type);
    public void Stop() => m_animationHandler.Stop();

    public virtual void TakeDamage(int dmg)
    {
        m_stats.Health -= dmg;
    }

    private void OnMouseDown()
    {
        TakeDamage(PlayerData.Instance.FinalDamage);
        m_animationHandler.PlayTemporary(AnimationType.Hurt, 1f);
    }
}