using System;
using System.Collections.Generic;
using UnityEngine;

internal class UnitAnimationHandler
{
    private readonly List<AnimationSheet> m_animations;

    private ScriptableSpriteSheet m_selectedAnimation;
        
    private readonly SpriteRenderer m_spriteRenderer;

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

    public void Stop() => m_selectedAnimation.Stop();

    private void ChangeSprite(Sprite newSprite)
    {
        m_spriteRenderer.sprite = newSprite;
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
}