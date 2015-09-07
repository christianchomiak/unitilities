/// <summary>
/// ColorExtensions v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Functions that facilitate the use of Unity's Color. 
/// </summary>

using UnityEngine;

namespace Unitilities
{
    public static class ColorExtentions
    {

        public static string ToHex(this Color c, bool includeAlpha = false)
        {
            string hex = "";

            int size = includeAlpha ? 4 : 3;

            byte[] bytes = new byte[size];

            for (int i = 0; i < size; i++)
            {
                bytes[i] = (byte) (c[i] * 255f);
            }

            hex = System.BitConverter.ToString(bytes);
            hex = hex.Replace("-", "");
            return hex;
        }

    }
}