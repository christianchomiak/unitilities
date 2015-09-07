/// <summary>
/// IComparableExtensions v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Functions that facilitate the use of IComparable objects. 
/// </summary>

using System;

namespace Unitilities
{

    public static class IComparableExtensions
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

}