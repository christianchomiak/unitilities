using System;
using UnityEngine;
using System.Collections;

public static class IComparableHelper
{
    /// <summary>
    /// Checks if the element is contained in a range (borders included)
    /// </summary>
    /// <typeparam name="T">Type of the element</typeparam>
    /// <param name="actual">The element to be tested</param>
    /// <param name="lower">The lower value of the range</param>
    /// <param name="upper">The highest value of the range</param>
    /// <returns></returns>
    public static bool Between<T>(this T actual, T lower, T upper) where T : IComparable<T>
    {
        return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
    }

    /// <summary>
    /// Checks if the element is contained in a range (borders excluded)
    /// </summary>
    /// <typeparam name="T">Type of the element</typeparam>
    /// <param name="actual">The element to be tested</param>
    /// <param name="lower">The lower value of the range</param>
    /// <param name="upper">The highest value of the range</param>
    /// <returns></returns>
    public static bool StrictlyBetween<T>(this T actual, T lower, T upper) where T : IComparable<T>
    {
        return actual.CompareTo(lower) > 0 && actual.CompareTo(upper) < 0;
    }

}
