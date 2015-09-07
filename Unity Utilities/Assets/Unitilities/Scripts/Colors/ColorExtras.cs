/// <summary>
/// RandomColor v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// API to get random colors a given hue, saturation or value as a starting point.
/// </summary>

using UnityEngine;

namespace Unitilities.Colors
{

    public static class RandomColor
    {
        /// <summary>
        /// Generates a RGB hsvColor using random hue, saturation, value (brightness)
        /// </summary>
        /// <param name="useGoldenRatio">'True': the golden ration will be used to better randomize the hue</param>
        /// <returns>RGB hsvColor</returns>
        /*public static Color HSV_RandomColor(bool useGoldenRatio = false) // 0.5, 0.95
        {
            return WithSaturationValue(Rnd.value, Rnd.value, useGoldenRatio);
        }*/

        /// <summary>
        /// Generates a RGB hsvColor using random hue and saturation, and specified value (brightness)
        /// </summary>
        /// <param name="value">Brightness of the hsvColor</param>
        /// <param name="useGoldenRatio">'True': the golden ration will be used to better randomize the hue</param>
        /// <returns>RGB hsvColor</returns>
        public static Color WithValue(float value, bool useGoldenRation = false) // 0.5, 0.95
        {
            return WithSaturationValue(UnityEngine.Random.value, value, useGoldenRation);
        }

        /// <summary>
        /// Generates a RGB hsvColor using random hue and value (brightness), and specified saturation
        /// </summary>
        /// <param name="saturation">Saturation of the hsvColor</param>
        /// <param name="useGoldenRatio">'True': the golden ration will be used to better randomize the hue</param>
        /// <returns>RGB hsvColor</returns>
        public static Color WithSaturation(float saturation, bool useGoldenRation = false) // 0.5, 0.95
        {
            return WithSaturationValue(saturation, UnityEngine.Random.value, useGoldenRation);
        }

        /// <summary>
        /// Generates a RGB hsvColor using random hue and specified saturation and value (brightness)
        /// </summary>
        /// <param name="saturation">Saturation of the hsvColor</param>
        /// <param name="value">Brightness of the hsvColor</param>
        /// <param name="useGoldenRatio">'True': the golden ration will be used to better randomize the hue</param>
        /// <returns>RGB hsvColor</returns>
        public static Color WithSaturationValue(float saturation, float value, bool useGoldenRatio = false)
        {
            //Rangos: 0 - 0.16667 - 0.33337 - 0.5 - 0.66667 - 0.83337 - 1
            float newHue = UnityEngine.Random.value;

            if (useGoldenRatio)
            {
                float goldenRadioConj = 0.618033988749895f;
                newHue += goldenRadioConj;
                newHue %= 1;
            }

            return HSVColor.ToRGB(newHue, saturation, value);
        }

        /// <summary>
        /// Generates a RGB hsvColor using random saturation and value (brightness) and specified hue and customAlpha
        /// </summary>
        /// <param name="hue">Hue of the hsvColor</param>
        /// <param name="customAlpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
        /// <returns>RGB hsvColor</returns>
        public static Color WithHue(float hue, float alpha = 1f)
        {
            return HSVColor.ToRGB(hue, UnityEngine.Random.value, UnityEngine.Random.value, alpha);
        }

        /// <summary>
        /// Generates a RGB hsvColor using random saturation and value (brightness) and specified hue and customAlpha
        /// </summary>
        /// <param name="hue">Hue of the hsvColor</param>
        /// <param name="saturation">Saturation of the hsvColor</param>
        /// <param name="customAlpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
        /// <returns>RGB hsvColor</returns>
        public static Color WithHueSaturation(float hue, float saturation, float alpha = 1)
        {
            return HSVColor.ToRGB(hue, saturation, UnityEngine.Random.value, alpha);
        }

        /// <summary>
        /// Generates a RGB hsvColor using random saturation and value (brightness) and specified hue and customAlpha
        /// </summary>
        /// <param name="hue">Hue of the hsvColor</param>
        /// <param name="value">Brightness of the hsvColor</param>
        /// <param name="customAlpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
        /// <returns>RGB hsvColor</returns>
        public static Color WithHueValue(float hue, float value, float alpha = 1)
        {
            return HSVColor.ToRGB(hue, UnityEngine.Random.value, value, alpha);
        }
    }


}