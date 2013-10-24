using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public static class StringExtensions
{

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

}
