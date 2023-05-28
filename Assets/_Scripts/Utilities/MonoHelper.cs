using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to help non-MonoBehaviour classes to use MonoBehaviour functions. Must be placed in a gameObject in scene
/// </summary>
public class MonoHelper : StaticInstance<MonoHelper>
{
    public bool RunInvokeInfinitely = true;
    
    /// <summary>
    /// Stops all currently running coroutines
    /// </summary>
    public void StopAll() => StopAllCoroutines();
    
    /// <summary>
    /// Repeatedly invoke <paramref name="action"/> every <paramref name="interval"/>
    /// while <see cref="RunInvokeInfinitely"/> is true or until <see cref="StopAll"/> is called
    /// </summary>
    public void InvokeRepeat(Action action, float interval, float delay = 0)
    {
        StartCoroutine(Invoke(action, delay, interval));
    }

    private IEnumerator Invoke(Action action, float delay, float interval)
    {
        yield return new WaitForSeconds(delay);
        do
        {
            action.Invoke();
            yield return new WaitForSeconds(interval);
        } while (RunInvokeInfinitely);
    }
}
