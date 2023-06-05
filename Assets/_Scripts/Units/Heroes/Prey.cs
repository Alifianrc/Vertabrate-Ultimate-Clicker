using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : PreyUnitBase
{
    [SerializeField] private AudioClip _someSound;

    void Start()
    {
        // Example usage of a static system
        AudioSystem.Instance.PlaySound(_someSound);

        Play(AnimationType.Move);
    }
}
