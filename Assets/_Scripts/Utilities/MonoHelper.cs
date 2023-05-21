using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHelper : StaticInstance<MonoHelper>
{
    public bool RunInvokeInfinitely = true;
    public void StopAll() => StopAllCoroutines();
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
