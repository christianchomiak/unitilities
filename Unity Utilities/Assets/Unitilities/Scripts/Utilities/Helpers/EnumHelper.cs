using UnityEngine;
using System.Collections;

public class EnumHelper
{

    public static T GetRandomEnum<T>(int limit)
    {
        System.Array A = System.Enum.GetValues(typeof(T));

        if (limit < 0 || limit >= A.Length)
        {
            return GetRandomEnum<T>();
        }

        T V = (T)A.GetValue(UnityEngine.Random.Range(0, limit));
        return V;
    }

    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

}
