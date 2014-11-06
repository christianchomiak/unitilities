using System;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public static class PrimitiveHelper
{
    #region int

    /// <summary>
    /// Returns the bool-equivalent of the value
    /// </summary>
    /// <param name="value"></param>
    /// <returns>True if the value is positive, else false</returns>
    public static bool ToBool(this int value)
    {
        return value > 0;
    }

    /// <summary>
    /// Returns 1 or -1 at random.
    /// </summary>
    /// <returns>1 or -1</returns>
    public static int RandomSign()
    {
        int sign = UnityEngine.Random.value > 0.5f ? 1 : -1;
        return sign;
    }

    #endregion


    #region float

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

    #endregion


    #region bool

    /// <summary>
    /// Returns the bool-equivalent of the value
    /// </summary>
    /// <param name="value"></param>
    /// <returns>True if the value is positive, else false</returns>
    public static int ToInt(this bool value)
    {
        if (value)
            return 1;
        else
            return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="probabilityOfFalse"></param>
    /// <returns></returns>
    public static bool RandomBool(float probabilityOfFalse = 0.5f)
    {
        bool b = UnityEngine.Random.value >= probabilityOfFalse ? true : false;
        return b;
    }

    #endregion

    
    #region string

    public static string FillNumberWithLeftZeros(int number, int maxDigits)
    {
        string filledNumber = number.ToString();
        int zerosToAdd = maxDigits - filledNumber.Length;
        for (int i = 0; i < zerosToAdd; i++)
        {
            filledNumber = "0" + filledNumber;
        }

        return filledNumber;
    }

    /// <summary>
    /// Determines if a string has a valid email address structure
    /// </summary>
    /// <param name="s">String to be tested</param>
    /// <returns>'True' if is a valid address, otherwise 'False'</returns>
    public static bool IsValidEmailAddress(this string s)
    {
        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(s);
    }

    /// <summary>
    /// Count all words in a given string
    /// </summary>
    /// <param name="input">string to begin with</param>
    /// <returns>int</returns>
    public static int WordCount(this string input)
    {
        var count = 0;
        try
        {
            // Exclude whitespaces, Tabs and line breaks
            var re = new Regex(@"[^\s]+");
            var matches = re.Matches(input);
            count = matches.Count;
        }
        catch
        {
        }
        return count;
    }

    #endregion

}
