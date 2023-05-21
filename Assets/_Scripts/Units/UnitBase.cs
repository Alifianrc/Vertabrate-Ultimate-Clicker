using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This will share logic for any unit on the field. Could be friend or foe, controlled or not.
/// Things like taking damage, dying, animation triggers etc
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public abstract class UnitBase : MonoBehaviour {
    public Stats Stats { get; private set; }

    private UnitAnimationHandler m_animationHandler;

    public void Init(Stats stats, List<AnimationSheet> animations)
    {
        m_animationHandler = new UnitAnimationHandler(animations, GetComponent<SpriteRenderer>());
        Stats = stats;
    }

    public void Play(AnimationType type) => m_animationHandler.Play(type);
    public void Stop() => m_animationHandler.Stop();

    public virtual void TakeDamage(int dmg) {
        
    }
}