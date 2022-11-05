using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SeededRuns.Extensions;

public static class UnityExtensions
{
    // https://answers.unity.com/questions/530178/how-to-get-a-component-from-an-object-and-add-it-t.html - thanks
    internal static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch
                {
                    // In case of NotImplementedException being thrown.
                }
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }

    // https://answers.unity.com/questions/530178/how-to-get-a-component-from-an-object-and-add-it-t.html - thanks
    internal static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }

    internal static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();

        if (!component)
            component = gameObject.AddComponent<T>();

        return component;
    }
    
    internal static T? GetChildComponentByName<T>(this MonoBehaviour mb, string name) where T : Component
    {
        return mb.GetComponentsInChildren<T>(true).FirstOrDefault(component => component.gameObject.name == name);
    }
    
    internal static T? GetChildComponentByName<T>(this GameObject go, string name) where T : Component
    {
        return go.GetComponentsInChildren<T>(true).FirstOrDefault(component => component.gameObject.name == name);
    }
}