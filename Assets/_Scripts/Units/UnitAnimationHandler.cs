using System;
using System.Collections.Generic;
using System.Timers;
using NoSuchStudio.Common;
using UnityEngine;

[Serializable]
internal class UnitAnimationHandler : ClassWithLogger
{
    private List<AnimationSheet> m_animations;

    [SerializeField] private SpriteSheet m_selectedAnimation;
    
    private readonly SpriteRenderer m_spriteRenderer;

    [SerializeField] private Sprite m_currentSprite;

    public UnitAnimationHandler(List<AnimationSheet> animations, SpriteRenderer renderer)
    {
        m_animations = animations;
        m_spriteRenderer = renderer;
    }
        
    public void Play(AnimationType type, bool fromBeginning = false)
    {
        var anim = GetAnimation(type);
        SelectAnimation(anim);
        anim.Play(fromBeginning);
    }

    public void PlayTemporary(AnimationType type, float time, bool fromBeginning = false)
    {
        var current = GetType(m_selectedAnimation);
        if(current == type) return;
        Play(type, fromBeginning);
        MonoHelper.Instance.Run(() => Play(current, fromBeginning), time);
    }

    /// <summary>
    /// Stops the currently playing animation
    /// </summary>
    public void Stop()
    {
        if(m_selectedAnimation == null) return;
        m_selectedAnimation.Stop();
    }

    private void ChangeSprite(Sprite newSprite)
    {
        //Log("Change sprite to " + newSprite);
        m_spriteRenderer.sprite = newSprite;
        m_currentSprite = newSprite;
    }

    private void SelectAnimation(SpriteSheet animation)
    {
        DeselectAnimation();

        m_selectedAnimation = animation;
        m_selectedAnimation.OnSpriteIndexChanged += ChangeSprite;
    }

    private void DeselectAnimation()
    {
        if (m_selectedAnimation != null)
        {
            m_selectedAnimation.OnSpriteIndexChanged -= ChangeSprite;
            m_selectedAnimation = null;
        }
    }

    private SpriteSheet GetAnimation(AnimationType type)
    {
        foreach (var anim in m_animations)
        {
            if (anim.type == type)
            {
                return anim.Sheet;
            }
        }

        //return null;
        throw new Exception($"Animation with type = {type} not found");
    }

    private AnimationType GetType(SpriteSheet spriteSheet)
    {
        foreach (var anim in m_animations)
        {
            if (anim.SheetData == spriteSheet.Data)
            {
                return anim.type;
            }
        }

        throw new($"Animation {spriteSheet} not found");
    }
}