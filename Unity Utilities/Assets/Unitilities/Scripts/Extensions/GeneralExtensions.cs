using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GeneralExtensions
{
    /// <summary>
    /// Applies an Action to each element of a list
    /// </summary>
    /// <typeparam name="T">Type of each element of the list</typeparam>
    /// <param name="list">Elements to aplly the action on</param>
    /// <param name="action">Action to be performed on each element</param>
    public static void ForEachElement<T>(this List<T> list, Action<T> action)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //action.Invoke(list[i]);
            action(list[i]);
        }
    }

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


    #region Random

    /// <summary>
    /// Returns a random element of an array.
    /// </summary>
    /// <typeparam name="T">Type of the array</typeparam>
    /// <param name="array">Array containing the element</param>
    /// <returns>A random element of the array</returns>
    public static T GetRandom<T>(this T[] array)
    {
        if (array.Length == 0)
            throw new System.ArgumentException("Array is empty", "array"); 
        //Debug.LogException(new Exception("Empty list"));
            //Debug.LogError("Empty list");

        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    /// <summary>
    /// Returns a random element of a List.
    /// </summary>
    /// <typeparam name="T">Type of the list</typeparam>
    /// <param name="list">List containing the element</param>
    /// <returns>A random element of the list</returns>
    public static T GetRandom<T>(this List<T> list)
    {
        if (list.Count == 0)
            throw new System.ArgumentException("List is empty", "list");
        
        //Debug.LogError("Empty list");

        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Modifies an array by shuffling it using the Fisher–Yates method.
    /// </summary>
    /// <typeparam name="T">Type of the array</typeparam>
    /// <param name="array">The array to be shuffled</param>
    public static void Shuffle<T>(this T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }
    }

    /// <summary>
    /// Modifies a list by shuffling it using the Fisher–Yates method.
    /// </summary>
    /// <typeparam name="T">Type of the list</typeparam>
    /// <param name="list">The list to be shuffled</param>
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = list[j];
            list[j] = list[i];
            list[i] = temp;
        }
    }

    /// <summary>
    /// Returns a shuffled copy of an array, using the Fisher–Yates method.
    /// </summary>
    /// <typeparam name="T">Type of the array</typeparam>
    /// <param name="array">Original array to be copied</param>
    /// <returns>A shuffled and shallow copy of the original array</returns>
    public static T[] GetShuffledCopy<T>(this T[] array)
    {
        T[] copy = (T[])array.Clone();

        copy.Shuffle();

        return copy;
    }

    /// <summary>
    /// Returns a shuffled copy a list, using the Fisher–Yates method.
    /// </summary>
    /// <typeparam name="T">Type of the list</typeparam>
    /// <param name="list">Original list to be copied.</param>
    /// <returns>A shuffled and shallow copy of the original list</returns>
    public static List<T> GetShuffledCopy<T>(this List<T> list)
    {
        List<T> copy = new List<T>(list);

        copy.Shuffle();

        return copy;
    }

    #endregion
}
