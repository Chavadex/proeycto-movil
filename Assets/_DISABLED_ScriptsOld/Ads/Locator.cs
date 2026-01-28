using System;
using UnityEngine;
using System.Collections.Generic;

public static class Locator
{
    public static Dictionary<Type, object> Services = new();

    public static T Get<T>()
    {
        Debug.Log("Get T");
        if (Services.TryGetValue(typeof(T), out var service))
        {
            Debug.Log("Se encontro T");
            return (T)service;
        }
        throw new Exception($"Service of type {typeof(T)} not found");
    }

    public static void Set<T>(T service)
    {
        if (Services.ContainsKey(typeof(T)))
        {
            Services[typeof(T)] = service;
            return;
        }
        Services.Add(typeof(T), service);
    }

    public static void Clear()
    {
        Services.Clear();
    }


    public static void Remove<T>()
    {
        if (Services.ContainsKey(typeof(T)))
        {
            Services.Remove(typeof(T));
        }
    }

}