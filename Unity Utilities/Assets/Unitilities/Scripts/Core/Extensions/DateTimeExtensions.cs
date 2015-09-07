/// <summary>
/// DateTimeExtensions v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Functions for manipulating Date/Time.
/// </summary>

using System;

namespace Unitilities
{

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Calculates the UNIX timestamp of a System.DateTime object
        /// </summary>
        /// <returns>The Unix timestamp</returns>
        public static long ToUnixTimestamp(this DateTime date)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan unixTimeSpan = date - unixEpoch;

            return (long) unixTimeSpan.TotalSeconds;
        }
    }

}