using UnityEngine;
using System.Collections;

public static class EnumHelper
{

    public static T GetRandomEnum<T>(int limit)
    {
        System.Array A = System.Enum.GetValues(typeof(T));

        if (A.Length == 0)
        {
            throw new System.ArgumentException("Enum " + typeof(T).ToString() + " is empty", "array");
        }

        if (limit < 0 || limit >= A.Length)
        {
            return GetRandomEnum<T>();
        }

        T V = (T) A.GetValue(UnityEngine.Random.Range(0, limit));
        return V;
    }

    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));

        if (A.Length == 0)
        {
            throw new System.ArgumentException("Enum " + typeof(T).ToString() + " is empty", "array"); 
        }

        T V = (T) A.GetValue(UnityEngine.Random.Range(0, A.Length));

        return V;
    }

}
