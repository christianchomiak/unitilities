using UnityEngine;
using System.Collections;

public static class NumberExtensions
{
    /// <summary>
    /// Given an original value, returns the value inside a range.
    /// </summary>
    /// <param name="value">Value to be wrapped</param>
    /// <param name="min">Min boundary of the range</param>
    /// <param name="max">Max boundary of the range</param>
    /// <returns></returns>
    public static float Wrapped(this float value, float min, float max)
    {
        if (value > max)
            return (value - max) + min;
        
        if (value < min)
            return max - (min - value);
        
        return value;
    }
}
