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
    public void RunRepeat(Action action, float interval, float delay = 0)
    {
        StartCoroutine(RunRepeatedly(action, delay, interval));
    }

    public void Run(Action action, float delay = 0)
    {
        StartCoroutine(Coroutine(action, delay));
    }

    public void Run(IEnumerator action, float delay = 0)
    {
        StartCoroutine(Coroutine(action, delay));
    }

    private static IEnumerator Coroutine(Action action, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    private static IEnumerator Coroutine(IEnumerator action, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        yield return action;
    }

    private IEnumerator RunRepeatedly(Action action, float delay, float interval)
    {
        yield return new WaitForSeconds(delay);
        do
        {
            action.Invoke();
            yield return new WaitForSeconds(interval);
        } while (RunInvokeInfinitely);
    }
}
