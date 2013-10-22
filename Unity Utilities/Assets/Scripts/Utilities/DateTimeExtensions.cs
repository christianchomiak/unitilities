using System;

public static class DateTimeExtensions 
{
    /// <summary>
    /// Converts a System.DateTime object to Unix timestamp
    /// </summary>
    /// <returns>The Unix timestamp</returns>
    public static long ToUnixTimestamp(this DateTime date)
    {
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
        TimeSpan unixTimeSpan = date - unixEpoch;

        return (long)unixTimeSpan.TotalSeconds;
    }

}
