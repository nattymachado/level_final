using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void SafeInvoke(this Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public static void SafeInvoke<T>(this Action<T> action, T obj)
    {
        if (action != null)
        {
            action.Invoke(obj);
        }
    }

    public static void SafeInvoke<T, U>(this Action<T,U> action, T objT, U objU)
    {
        if (action != null)
        {
            action.Invoke(objT, objU);
        }
    }
}
