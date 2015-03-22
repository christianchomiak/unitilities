/// <summary>
/// ColorHelper v1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Functions that facilitate the manipulation of Unitiy's Color.
/// </summary>

using UnityEngine;
using System.Collections;

namespace Unitilities
{
    public class ColorHelper
    {
        public static Color ColorFromHex(string hex)
        {
            Color c = new Color();

            byte[] hexBytes = StringToByteArrayFastest(hex);

            for (int i = 0; i < hexBytes.Length && i < 4; i++)
            {
                c[i] = ((float) hexBytes[i]) / 255f;
                Debug.Log(hexBytes[i].ToString());
            }

            return c;
        }

        private static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new System.ArgumentException("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte) ((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        private static int GetHexVal(char hex)
        {
            int val = (int) hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        //Based on http://wiki.unity3d.com/index.php?title=HSBColor
        /// <summary>
        /// Generates a RGB hsvColor using specified hue, saturation, value (brightness) and  hue and alpha
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        /// <param name="alpha"></param>
        /// <returns>RGB hsvColor</returns>
        /// //[Deprecated] This function uses h, s, v = [0, 1]
        /*public static Color HSVToRGB2(float hue, float saturation, float value, float alpha = 1.0f) //value = brightness
        {
            float r = value;
            float g = value;
            float b = value;

            if (saturation != 0f)
            {
                float max = value;
                float dif = value * saturation;
                float min = value - dif;

                float h = hue * 360f;

                if (h < 60f)
                {
                    r = max;
                    g = h * dif / 60f + min;
                    b = min;
                }
                else if (h < 120f)
                {
                    r = -(h - 120f) * dif / 60f + min;
                    g = max;
                    b = min;
                }
                else if (h < 180f)
                {
                    r = min;
                    g = max;
                    b = (h - 120f) * dif / 60f + min;
                }
                else if (h < 240f)
                {
                    r = min;
                    g = -(h - 240f) * dif / 60f + min;
                    b = max;
                }
                else if (h < 300f)
                {
                    r = (h - 240f) * dif / 60f + min;
                    g = min;
                    b = max;
                }
                else if (h <= 360f)
                {
                    r = max;
                    g = min;
                    b = -(h - 360f) * dif / 60 + min;
                }
                else
                {
                    r = 0;
                    g = 0;
                    b = 0;
                }
            }

            return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), alpha);
        }*/


        //http://www.rapidtables.com/convert/hsvColor/rgb-to-hsv.htm
        /// <summary>
        /// Returns the ToHSV values of a RGB hsvColor
        /// </summary>
        /// <param name="hsvColor">RGB hsvColor</param>
        /// <returns>Vector3 containing the ToHSV representation of the RGB hsvColor</returns>
        /*public static HSVColor RGBToHSV(Color color) //, out float hue, out float s, out float v)
        {
            return HSVColor.FromRGB(color);
            //return RGBToHSV(hsvColor.r, hsvColor.g, hsvColor.b, hsvColor.a);
        }*/
    }
}