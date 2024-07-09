using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CallHandler 
{
    public static IEnumerator Callback(IEnumerator numerator, Action After)
    {
        yield return numerator;
        After?.Invoke();
    }
    /// <summary>
    /// Dipake pas butuh call sambil nunggu script lain jalan
    /// </summary>
    public static IEnumerator WaitTill(Func<bool> Condition, Action CallAfterWait)
    {
        Debug.Log("Waiting call");
        yield return new WaitUntil(Condition);
        Debug.Log("DOne");
        CallAfterWait?.Invoke();
    }
    
    public static IEnumerator Wait<T>(Func<T, IEnumerator> exp, Action CallAfterWait)
    {
        yield return exp;

    }
}
