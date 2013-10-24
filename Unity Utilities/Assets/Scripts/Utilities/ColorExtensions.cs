using UnityEngine;
using System.Collections;

public static class ColorExtensions
{

    /// <summary>
    /// Returns the HSV values of a RGB color
    /// </summary>
    /// <param name="color">RGB color</param>
    /// <returns>Vector3 containing the HSV representation of the RGB color</returns>
    public static Vector3 GetHSV(this Color color) //, out float h, out float s, out float v)
    {
        float h, s, v;

        float min = Mathf.Min(Mathf.Min(color.r, color.g), color.b);
        float max = Mathf.Max(Mathf.Max(color.r, color.g), color.b);
        float delta = max - min;

        // value is our max color
        v = max;

        // saturation is percent of max
        if (!Mathf.Approximately(max, 0))
            s = delta / max;
        else
        {
            // all colors are zero, no saturation and hue is undefined
            s = 0;
            h = -1;
            return new Vector3(h, s, v);
        }

        // grayscale image if min and max are the same
        if (Mathf.Approximately(min, max))
        {
            v = max;
            s = 0;
            h = -1;
            return new Vector3(h, s, v);
        }

        // hue depends which color is max (this creates a rainbow effect)
        if (color.r == max)
            h = (color.g - color.b) / delta;            // between yellow & magenta
        else if (color.g == max)
            h = 2 + (color.b - color.r) / delta;                // between cyan & yellow
        else
            h = 4 + (color.r - color.g) / delta;                // between magenta & cyan

        // turn hue into 0-360 degrees
        h *= 60;
        if (h < 0)
            h += 360;

        return new Vector3(h, s, v);
    }

}
