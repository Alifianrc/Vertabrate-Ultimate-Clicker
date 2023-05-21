using System;
using System.Timers;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers 
{
    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t) {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }
    
    /// <summary>
    /// Usage:
    /// <code>var timer = RunEvery(ActionToRun, 1000);</code>
    /// </summary>
    /// <returns>Returns a timer object which can be stopped and disposed.</returns>
    public static Timer RunEvery(Action action, int intervalMillisecond)
    {
        var tmr = new Timer();
        tmr.Elapsed += (sender, args) => action();
        tmr.AutoReset = true;
        tmr.Interval = intervalMillisecond;
        tmr.Start();
        Debug.Log("RunEvery");

        return tmr;
    }
    
    /// <summary>
    /// Usage:
    /// <code>var timer = RunEvery(ActionToRun, 1000);</code>
    /// This variant has an <see cref="ElapsedEventArgs"/> that could be used to see signal time
    /// </summary>
    /// <returns>Returns a timer object which can be stopped and disposed.</returns>
    public static Timer RunEvery(Action<ElapsedEventArgs> action, int intervalMillisecond)
    {
        var tmr = new Timer();
        tmr.Elapsed += (sender, args) => action(args);
        tmr.AutoReset = true;
        tmr.Interval = intervalMillisecond;
        tmr.Start();

        return tmr;
    }
}
