using System;
using System.Collections.Generic;
using System.Timers;
using NoSuchStudio.Common;
using UnityEngine;

[Serializable]
internal class UnitAnimationHandler : ClassWithLogger
{
    private List<AnimationSheet> m_animations;

    [SerializeField] private ScriptableSpriteSheet m_selectedAnimation;
    
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

    private void SelectAnimation(ScriptableSpriteSheet animation)
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

    private ScriptableSpriteSheet GetAnimation(AnimationType type)
    {
        foreach (var anim in m_animations)
        {
            if (anim.Type == type)
            {
                return anim.SpriteSheet;
            }
        }

        //return null;
        throw new Exception($"Animation with type = {type} not found");
    }

    private AnimationType GetType(ScriptableSpriteSheet spriteSheet)
    {
        foreach (var anim in m_animations)
        {
            if (anim.SpriteSheet == spriteSheet)
            {
                return anim.Type;
            }
        }

        throw new Exception($"Animation not found");
    }
}