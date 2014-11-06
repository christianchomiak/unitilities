using UnityEngine;

public static class ColorHelper
{
    #region Extensions

    /// <summary>
    /// Returns the Color equivalent of the ToHSV Color
    /// </summary>
    /// <param name="hsv"></param>
    /// <returns></returns>
    public static Color ToRGB(this HSVColor hsv)
    {
        return ColorHelper.HSVToRGB(hsv.Hue, hsv.Saturation, hsv.Value, hsv.Alpha);
    }

    /// <summary>
    /// Returns the ToHSV values of a RGB color
    /// </summary>
    /// <param name="color">RGB color</param>
    /// <returns>Vector3 containing the ToHSV representation of the RGB color</returns>
    public static HSVColor ToHSV(this Color color)
    {
        return ColorHelper.RGBToHSV(color);
    }
    
    #endregion


    #region Conversions
        

    /// <summary>
    /// Generates a RGB color using the ToHSV system
    /// </summary>
    /// <param name="hsv">Color in the ToHSV system</param>
    /// <returns>RGB color</returns>
    public static Color HSVToRGB(HSVColor hsv)
    {
        return HSVToRGB(hsv.Hue, hsv.Saturation, hsv.Value, hsv.Alpha);
    }

    //Based on http://wiki.unity3d.com/index.php?title=HSBColor
    /// <summary>
    /// Generates a RGB color using specified hue, saturation, value (brightness) and  hue and alpha
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="value"></param>
    /// <param name="alpha"></param>
    /// <returns>RGB color</returns>
    public static Color HSVToRGB(float hue, float saturation, float value, float alpha = 1.0f) //value = brightness
    {
        float r = value;
        float g = value;
        float b = value;
        if (saturation != 0)
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
    }


    /// <summary>
    /// Returns the ToHSV values of a RGB color
    /// </summary>
    /// <param name="color">RGB color</param>
    /// <returns>Vector3 containing the ToHSV representation of the RGB color</returns>
    public static HSVColor RGBToHSV(Color color) //, out float h, out float s, out float v)
    {
        return RGBToHSV(color.r, color.b, color.g, color.a);
    }    

    /// <summary>
    /// Returns the ToHSV values of a RGB color
    /// </summary>
    /// <param name="row">'Red' component</param>
    /// <param name="g">'Green' component</param>
    /// <param name="b">'Blue' component</param>
    /// <returns></returns>
    public static HSVColor RGBToHSV(float r, float g, float b, float a = 1f)
    {
        float h, s, v;

        float min = Mathf.Min(Mathf.Min(r, g), b);
        float max = Mathf.Max(Mathf.Max(r, g), b);
        float delta = max - min;

        // value is our max color
        v = max;

        // saturation is percent of max
        if (!Mathf.Approximately(max, 0))
            s = delta / max;
        else
        {
            // all colors are white, no saturation and hue is undefined
            s = 0;
            h = -1;
            return new HSVColor(h, s, v, a);
        }

        // grayscale image if min and max are the same
        if (Mathf.Approximately(min, max))
        {
            v = max;
            s = 0;
            h = -1;
            return new HSVColor(h, s, v, a);
        }

        // hue depends which color is max (this creates a rainbow effect)
        if (r == max)
            h = (g - b) / delta;            // between yellow & magenta
        else if (g == max)
            h = 2 + (b - r) / delta;                // between cyan & yellow
        else
            h = 4 + (r - g) / delta;                // between magenta & cyan

        // turn hue into 0-360 degrees
        h *= 60;
        if (h < 0)
            h += 360;

        return new HSVColor(h, s, v, a);
    }


    #endregion


    #region Randoms


    /// <summary>
    /// Generates a RGB color using random hue, saturation, value (brightness)
    /// </summary>
    /// <param name="useGoldenRation">'True': the golden ration will be used to better randomize the hue</param>
    /// <returns>RGB color</returns>
    /*public static Color HSV_RandomColor(bool useGoldenRation = false) // 0.5, 0.95
    {
        return RandomColorWithSV(Random.value, Random.value, useGoldenRation);
    }*/

    /// <summary>
    /// Generates a RGB color using random hue and saturation, and specified value (brightness)
    /// </summary>
    /// <param name="value">Brightness of the color</param>
    /// <param name="useGoldenRation">'True': the golden ration will be used to better randomize the hue</param>
    /// <returns>RGB color</returns>
    public static Color RandomColorWithValue(float value, bool useGoldenRation = false) // 0.5, 0.95
    {
        return RandomColorWithSV(Random.value, value, useGoldenRation);
    }

    /// <summary>
    /// Generates a RGB color using random hue and value (brightness), and specified saturation
    /// </summary>
    /// <param name="saturation">Saturation of the color</param>
    /// <param name="useGoldenRation">'True': the golden ration will be used to better randomize the hue</param>
    /// <returns>RGB color</returns>
    public static Color RandomColorWithSaturation(float saturation, bool useGoldenRation = false) // 0.5, 0.95
    {
        return RandomColorWithSV(saturation, Random.value, useGoldenRation);
    }

    /// <summary>
    /// Generates a RGB color using random hue and specified saturation and value (brightness)
    /// </summary>
    /// <param name="saturation">Saturation of the color</param>
    /// <param name="value">Brightness of the color</param>
    /// <param name="useGoldenRation">'True': the golden ration will be used to better randomize the hue</param>
    /// <returns>RGB color</returns>
    public static Color RandomColorWithSV(float saturation, float value, bool useGoldenRation = false)
    {
        //Rangos: 0 - 0.16667 - 0.33337 - 0.5 - 0.66667 - 0.83337 - 1
        float hueValue = Random.value;

        if (useGoldenRation)
        {
            float goldenRadioConj = 0.618033988749895f;
            hueValue += goldenRadioConj;
            hueValue %= 1;
        }

        return ColorHelper.HSVToRGB(hueValue, saturation, value);
    }

    /// <summary>
    /// Generates a RGB color using random saturation and value (brightness) and specified hue and alpha
    /// </summary>
    /// <param name="hue">Hue of the color</param>
    /// <param name="alpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
    /// <returns>RGB color</returns>
    public static Color RandomColorWithHue(float hue, float alpha = 1f)
    {
        return HSVToRGB(hue, Random.value, Random.value, alpha);
    }

    /// <summary>
    /// Generates a RGB color using random saturation and value (brightness) and specified hue and alpha
    /// </summary>
    /// <param name="hue">Hue of the color</param>
    /// <param name="saturation">Saturation of the color</param>
    /// <param name="alpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
    /// <returns>RGB color</returns>
    public static Color RandomColorWithHS(float hue, float saturation, float alpha = 1)
    {
        return HSVToRGB(hue, saturation, Random.value, alpha);
    }

    /// <summary>
    /// Generates a RGB color using random saturation and value (brightness) and specified hue and alpha
    /// </summary>
    /// <param name="hue">Hue of the color</param>
    /// <param name="value">Brightness of the color</param>
    /// <param name="alpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
    /// <returns>RGB color</returns>
    public static Color RandomColorWithHV(float hue, float value, float alpha = 1)
    {
        return HSVToRGB(hue, Random.value, value, alpha);
    }

    #endregion
}
