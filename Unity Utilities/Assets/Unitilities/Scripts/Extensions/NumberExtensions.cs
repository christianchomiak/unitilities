using UnityEngine;
using System.Collections;

public static class NumberExtensions
{
    public static float Wrapped(this float value, float min, float max)
    {
        if (value > max)
            return (value - max) + min;
        
        if (value < min)
            return max - (min - value);
        
        return value;
    }
}
