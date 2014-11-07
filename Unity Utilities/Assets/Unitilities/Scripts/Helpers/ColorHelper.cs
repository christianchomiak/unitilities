using UnityEngine;

public static class ColorHelper
{
    #region Extensions

    /// <summary>
    /// Returns the Color equivalent of the HSV Color
    /// </summary>
    /// <param name="hsv"></param>
    /// <returns></returns>
    public static Color ToRGB(this HSVColor hsv)
    {
        return ColorHelper.HSVToRGB(hsv.Hue, hsv.Saturation, hsv.Value, hsv.Alpha);
    }

    /// <summary>
    /// Returns the HSV values of a RGB color
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
    /// Generates a RGB color using the HSV system
    /// </summary>
    /// <param name="hsv">Color in the ToHSV system</param>
    /// <returns>RGB color</returns>
    public static Color HSVToRGB(HSVColor hsv)
    {
        return HSVToRGB(hsv.Hue, hsv.Saturation, hsv.Value, hsv.Alpha);
    }


    //http://www.rapidtables.com/convert/color/rgb-to-hsv.htm
    /// <summary>
    /// Generates a RGB color using specified hue, saturation, value (brightness) and  hue and alpha
    /// </summary>
    /// <param name="hue">[0f, 360f] degrees</param>
    /// <param name="saturation">[0f, 1f]</param>
    /// <param name="value">[0f, 1f]</param>
    /// <param name="alpha">[0f, 1f]</param>
    /// <returns>RGB color</returns>
    public static Color HSVToRGB(float hue, float saturation, float value, float alpha = 1.0f) //value = brightness
    {
        float r = 0f, g = 0f, b = 0f;

        hue = hue % 360f;
        saturation = Mathf.Clamp01(saturation);
        value = Mathf.Clamp01(value);

        float c = value * saturation;
        float x = c * (1f - Mathf.Abs(((hue / 60f) % 2) - 1f));
        float m = value - c;


        if (hue < 60f)
        {
            r = c;
            g = x;
        }
        else if (hue < 120f)
        {
            r = x;
            g = c;
        }
        else if (hue < 180f)
        {
            g = c;
            b = x;
        }
        else if (hue < 240f)
        {
            g = x;
            b = c;
        }
        else if (hue < 300f)
        {
            r = x;
            b = c;
        }
        else if (hue <= 360f)
        {
            r = c;
            b = x;
        }
        else
        {
            r = 0f;
            g = 0f;
            b = 0f;
        }

        return new Color(r + m, g + m, b + m, alpha);
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


    //http://www.rapidtables.com/convert/color/rgb-to-hsv.htm
    /// <summary>
    /// Returns the ToHSV values of a RGB color
    /// </summary>
    /// <param name="color">RGB color</param>
    /// <returns>Vector3 containing the ToHSV representation of the RGB color</returns>
    public static HSVColor RGBToHSV(Color color) //, out float hue, out float s, out float v)
    {
        return RGBToHSV(color.r, color.g, color.b, color.a);
    }    

    /// <summary>
    /// Returns the ToHSV values of a RGB color
    /// </summary>
    /// <param name="r">'Red' component</param>
    /// <param name="g">'Green' component</param>
    /// <param name="b">'Blue' component</param>
    /// <returns></returns>
    public static HSVColor RGBToHSV(float r, float g, float b, float a = 1f)
    {
        float h, s, v;

        float max = Mathf.Max(Mathf.Max(r, g), b);
        float min = Mathf.Min(Mathf.Min(r, g), b);

        float delta = max - min;

        // value is our max color
        v = max;


        if (Mathf.Approximately(max, 0f) || Mathf.Approximately(delta, 0f))
        {
            // all colors are white, no saturation and hue is undefined
            s = 0f;
            h = 0f;
            return new HSVColor(h, s, v, a);
        }
        else // saturation is percent of max
            s = delta / max;

        // grayscale image if min and max are the same
        /*if (Mathf.Approximately(min, max))
        {
            v = max;
            s = 0;
            hue = -1;
            return new HSVColor(hue, s, v, a);
        }*/

        //Debug.Log("max: " + max + ". r: " + r + ". g: " + g + ". b: " + b);


        // hue depends which color is max (this creates a rainbow effect)
        if (Mathf.Approximately(r, max)) // r == max)
            h = ((g - b) / delta) % 6f;            // between yellow & magenta
        else if (Mathf.Approximately(g, max)) //(g == max)
        {
            h = 2f + (b - r) / delta;                // between cyan & yellow
        }
        else
            h = 4f + (r - g) / delta;                // between magenta & cyan


        // turn hue into 0-360 degrees
        h *= 60f;
        if (h < 0f)
            h += 360f;

        /*s *= 255f;
        v *= 255f;*/

        //hue: [0, 360]
        //s, v: [0, 1]
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
