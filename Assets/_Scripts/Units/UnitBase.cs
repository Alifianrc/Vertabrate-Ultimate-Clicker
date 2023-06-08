using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NoSuchStudio.Common;
using UnityEngine;

/// <summary>
/// This will share logic for any unit on the field. Could be friend or foe, controlled or not.
/// Things like taking damage, dying, animation triggers etc
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public abstract class UnitBase : MonoBehaviourWithLogger 
{
    [SerializeField] private UnitAnimationHandler m_animationHandler;
    [SerializeField] private UnitMovement m_movement;
    [SerializeField] private Stats m_stats;
    public event Action<UnitBase> Dead;
    public Stats Stats => m_stats;
    public bool IsInitialized { get; private set; }
    public bool IsDead { get; private set; }

    public void Init(Stats stats, List<AnimationSheet> animations)
    {
        if(IsInitialized) return;
        
        m_animationHandler = new(animations, GetComponent<SpriteRenderer>());
        m_stats = stats;
        m_movement = new(transform, stats.speed);
        m_movement.Start();
        m_movement.PathComplete += MovementOnPathComplete;
        PlayAnimation(AnimationType.Move);
        IsInitialized = true;
    }

    private void MovementOnPathComplete() => m_movement.Start();

    private void OnMouseDown() => PlayerData.Instance.OnClickPrey(this);

    public void PlayAnimation(AnimationType type) => m_animationHandler.Play(type);
    public void StopAnimation() => m_animationHandler.Stop();
    public void StartMovement() => m_movement.Start();
    public void StopMovement() => m_movement.Stop();

    public virtual void TakeDamage(int dmg)
    {
        m_stats.health -= dmg;
        Log($"Damage: {dmg}. Health left: {m_stats.health}");
        m_animationHandler.PlayTemporary(AnimationType.Hurt, 1f);
        m_animationHandler.ChangeColorTemporary(Color.white, Color.red, 0.5f);

        if (m_stats.health <= 0 && !IsDead)
        {
            IsDead = true;
            Dead?.Invoke(this);
        }
    }

    public void SetVisible(bool visible) => m_animationHandler.SetVisible(visible ? 1 : 0.1f);

    private void OnDestroy() => m_animationHandler.Disable();
}